using Domain.Models;
namespace Services.Repository
{
    public interface ILoginRepository
    {
        IEnumerable<User> GetUsers();
    }
}
