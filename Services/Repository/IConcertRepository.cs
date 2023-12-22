using Domain.Models;


namespace Services.Repository
{
    public interface IConcertRepository
    {
        IEnumerable<Concerts> GetConcerts();
        Concerts GetConcerts(int id);
        int InsertConcerts(Concerts concert);
        int UpdateConcerts(Concerts concert);
        int DeleteConcerts(int id);
    }
}
