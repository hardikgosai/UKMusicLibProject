using DAL.EntityFramework;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{

    public class ConcertRepository : IConcertRepository
    {
        public readonly ApplicationDbContext applicationDbContext;

        public ConcertRepository(ApplicationDbContext _applicationDbContext)
        {
            applicationDbContext = _applicationDbContext;
        }
        public int DeleteConcerts(int id)
        {
            var concertToDelete = GetConcerts(id);
            if (concertToDelete == null)
            {
                throw new ArgumentNullException("Entity Missing");
            }
            applicationDbContext.Remove(concertToDelete);
            return applicationDbContext.SaveChanges();
        }

        public IEnumerable<Concerts> GetConcerts()
        {
            return this.applicationDbContext.Concerts.ToList();
        }

        public Concerts GetConcerts(int id)
        {
            return applicationDbContext.Concerts.SingleOrDefault(t => t.ConcertId == id);
        }

        public int InsertConcerts(Concerts concert)
        {
            this.applicationDbContext.Add(concert);
            return this.applicationDbContext.SaveChanges();
        }

        public int UpdateConcerts(Concerts concert)
        {
            if (concert == null)
            {
                throw new ArgumentNullException("Entity Missing");
            }
            applicationDbContext.Concerts.Update(concert);
            return applicationDbContext.SaveChanges();
        }

    }
}
