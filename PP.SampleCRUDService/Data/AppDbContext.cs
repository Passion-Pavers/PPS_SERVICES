using Microsoft.EntityFrameworkCore;
using PP.SampleCRUDService.Models.DbEntities;

namespace PP.SampleCRUDService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { 

        }

        public DbSet<Application> Applications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the mapping between entity and table
            modelBuilder.Entity<Application>(entity =>
            {
                entity.ToTable("Application");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd() // Configures Id as auto-increment
                      .UseIdentityColumn();
                entity.Property(e => e.ApplicationName).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Description).HasMaxLength(255);
                entity.Property(e => e.CreatedBy);
                entity.Property(e => e.CreatedOn);
                entity.Property(e => e.ModifiedBy);
                entity.Property(e => e.ModifiedOn);
            });
        }
    }
}
