using DAL.EntityFramework;
using Domain.Models;
using System.Diagnostics.Contracts;

namespace Services.Repository
{
    public class ArtistRepository : IArtistRepository
    {
        ApplicationDbContext applicationDbContext;

        public ArtistRepository(ApplicationDbContext _applicationDbContext)
        {
            this.applicationDbContext = _applicationDbContext;

        }
        public int DeleteArtist(long id)
        {
            return 1;
        }

        public Artist GetArtist(long id)
        {
            return this.applicationDbContext.Artists.FirstOrDefault(x=>x.ArtistId==id);
        }

        public IEnumerable<Artist> GetArtists()
        {
            return this.applicationDbContext.Artists.ToList();
        }

        public int InsertArtist(Artist artist)
        {
            this.applicationDbContext.Add(artist);
            return this.applicationDbContext.SaveChanges();
        }

        public int UpdateArtist(Artist artist)
        {
            if (artist == null)
            {
                throw new ArgumentNullException("Entity Missing");
            }
            applicationDbContext.Artists.Update(artist);
            return applicationDbContext.SaveChanges();
        }
    }
}
