using Microsoft.EntityFrameworkCore;
using PP.Services.Models;

namespace PP.Services.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Test> StringEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the mapping between entity and table
            modelBuilder.Entity<Test>()
                .ToTable("test")
                .Property(e => e.id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(); ;
        }
    }
}