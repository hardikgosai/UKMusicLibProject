using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Repository;

namespace UKMusicLibProject.APIController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        public readonly IContractRepository icontractRepository;
        public ContractController(IContractRepository _icontractRepository)
        {
            this.icontractRepository = _icontractRepository;
        }

        [HttpPost("CreateContract")]
        public ActionResult CreateContract(Contracts contract)
        {
            return Ok(this.icontractRepository.InsertContracts(contract));
        }



        [HttpGet("ContractList")]
        public ActionResult GetContractList()
        {
            return Ok(this.icontractRepository.GetContracts());
        }

        [HttpGet("SearchContractById")]
        public ActionResult SearchContract(int contractId)
        {
            return Ok(this.icontractRepository.GetContractById(contractId));
        }

        [HttpPut("UpdateContract")]
        public ActionResult UpdateContract(Contracts contracts)
        {
            return Ok(this.icontractRepository.UpdateContracts(contracts));
        }

        [HttpDelete("DeleteContract")]
        public ActionResult DeleteContract(int id)
        {
            return Ok(this.icontractRepository.DeleteContracts(id));
        }
    }
}
