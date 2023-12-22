using DAL.EntityFramework;
using Domain.Models;

namespace Services.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        public UserRepository(ApplicationDbContext _applicationDbContext)
        {
          applicationDbContext = _applicationDbContext;
        }
        public void DeleteUser(long id)
        {
            var user = this.applicationDbContext.Users.FirstOrDefault(u => u.UserId == id);
            var userDelete = this.applicationDbContext.Users.Remove(user);
            applicationDbContext.SaveChanges();
        }

        public User GetUser(long id)
        {
            var user = this.applicationDbContext.Users.FirstOrDefault(u => u.UserId == id);
            return user;
        }

        public IEnumerable<User> GetUsers()
        {
            List<User> lstUser = this.applicationDbContext.Users.ToList();
            return lstUser;
        }

        public int InsertUser(User user)
        {
            this.applicationDbContext.Users.Add(user);
            return applicationDbContext.SaveChanges();
        }

        public int UpdateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentException("You enterd on wrong user");
            }
            else
            {
                this.applicationDbContext.Users.Update(user);
                return applicationDbContext.SaveChanges();
            }
        }
    }
}
