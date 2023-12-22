using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Repository;
using UKMusicLibProject.DTO;

namespace UKMusicLibProject.APIController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConcertAPIController : ControllerBase
    {

        private readonly IConcertRepository iconcertRepository;
        public ConcertAPIController(IConcertRepository _iconcertRepository)
        {
            iconcertRepository = _iconcertRepository;
        }
        [HttpGet("SingleConcert")]
        public ActionResult GetSingleUsers(int id)
        {
            ConcertDTO concertDto = new ConcertDTO();
            Concerts concertEntity = iconcertRepository.GetConcerts(id);

            concertDto.ConcertId = concertEntity.ConcertId;
            concertDto.Date = concertEntity.Date;
            concertDto.Location = concertEntity.Location;
            concertDto.TicketSold = concertEntity.TicketSold;
            concertDto.RevenueGenerated = concertEntity.RevenueGenerated;

            return Ok(concertDto);

        }

        [HttpGet("ListConcerts")]
        public ActionResult ListConcerts()
        {
            List<ConcertDTO> lstConcertDTO = new List<ConcertDTO>();
            iconcertRepository.GetConcerts().ToList().ForEach(u =>
            {
                ConcertDTO concertDto = null;

                concertDto = new ConcertDTO()
                {
                    ConcertId = u.ConcertId,
                    Date = u.Date,
                    Location = u.Location,
                    TicketSold = u.TicketSold,
                    RevenueGenerated = u.RevenueGenerated
                };
                lstConcertDTO.Add(concertDto);
            });
            return Ok(lstConcertDTO);
        }

        [HttpPost("CreateConcert")]
        public int CreateConcert(ConcertDTO model)
        {
            Concerts concertEntity = new Concerts
            {
                Date = model.Date,
                Location = model.Location,
                TicketSold = model.TicketSold,
                RevenueGenerated = model.RevenueGenerated,

            };
            iconcertRepository.InsertConcerts(concertEntity);
            return 1;
        }
        [HttpPut("UpdateConcert")]
        public int UpdateConcert(ConcertDTO model)
        {
            Concerts concertEntity = new Concerts
            {
                ConcertId = model.ConcertId,
                Date = model.Date,
                Location = model.Location,
                TicketSold = model.TicketSold,
                RevenueGenerated = model.RevenueGenerated,

            };
            iconcertRepository.UpdateConcerts(concertEntity);
            return 1;
        }

        [HttpDelete("DeleteConcert")]
        public int DeleteUser(int id)
        {
            iconcertRepository.DeleteConcerts(id);
            return 1;
        }

    }
}
