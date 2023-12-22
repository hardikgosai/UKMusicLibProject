using Domain.Models;
using Microsoft.EntityFrameworkCore;
namespace DAL.EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions opt) : base(opt)
        {

        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Contracts> Contracts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Concerts> Concerts { get; set; }

        protected override void OnModelCreating(ModelBuilder oModelBuilder)
        {
            //Form this function, we can do the association for User to UserMap and UserProfile to UserProfileMap
            new UserMap(oModelBuilder.Entity<User>());
           
            
        }
    }
}
