using MedFutureAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedFutureAPI.Persistence
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
            
        }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasKey(x => x.Id);
        }
    }
}
