using Domain.Models;
namespace Services.Repository
{
    public interface IArtistRepository
    {
        IEnumerable<Artist> GetArtists();
        Artist GetArtist(long id);
        int InsertArtist(Artist Artist);
        int UpdateArtist(Artist Artist);
        int DeleteArtist(long id);
    }
}
