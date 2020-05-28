using Microsoft.EntityFrameworkCore;
using Users.API.Models;

namespace Users.API.Repositories
{
    public class AppDbContext : DbContext
    {
         public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {           
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(p => p.Id);
        }
    }
}