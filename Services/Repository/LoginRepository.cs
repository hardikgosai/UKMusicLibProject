using DAL.EntityFramework;
using Domain.Models;



namespace Services.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly ApplicationDbContext applicationDbcontext;
       
        public LoginRepository(ApplicationDbContext _applicationDbcontext)
        {
            this.applicationDbcontext = _applicationDbcontext;
        }

        public IEnumerable<User> GetUsers()
        {
            return applicationDbcontext.Users.ToList();
        }
    }
}
