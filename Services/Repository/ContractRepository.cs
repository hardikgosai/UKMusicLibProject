using DAL.EntityFramework;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class ContractRepository : IContractRepository
    {
        public readonly ApplicationDbContext applicationDbContext;

        public ContractRepository(ApplicationDbContext _applicationDbContext)
        {
            applicationDbContext = _applicationDbContext;
        }

        public int DeleteContracts(long id)
        {
            // return applicationDbContext.Contracts.Remove(x=>x.)
            return 1;
        }

        public IEnumerable<Contracts> GetContracts()
        {
            return this.applicationDbContext.Contracts.ToList();
        }

        public Contracts GetContractById(long id)
        {
            return applicationDbContext.Contracts.SingleOrDefault(t => t.ContractId == id);
        }

        public int InsertContracts(Contracts contract)
        {
            this.applicationDbContext.Add(contract);
            return this.applicationDbContext.SaveChanges();
        }

        public int UpdateContracts(Contracts contract)
        {
            if (contract == null)
            {
                throw new ArgumentNullException("Entity Missing");
            }
            applicationDbContext.Contracts.Update(contract);
           return applicationDbContext.SaveChanges();
        }
    }
}
