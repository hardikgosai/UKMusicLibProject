using Domain.Models;
namespace Services.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User GetUser(long id);
        int InsertUser(User user);
        int UpdateUser(User user);
        void DeleteUser(long id);
    }
}
