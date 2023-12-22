using Domain.Models;
namespace Services.Repository
{
    public interface IContractRepository
    {
        IEnumerable<Contracts> GetContracts();
        Contracts GetContractById(long id);
        int InsertContracts(Contracts Contract);
        int UpdateContracts(Contracts Contract);
        int DeleteContracts(long id);
    }
}
