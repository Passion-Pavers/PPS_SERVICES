using Microsoft.EntityFrameworkCore;
using PP.CREDStroreService.Models.DbEntities;

namespace PP.CREDStroreService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Credentials> Credentials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the mapping between entity and table
            modelBuilder.Entity<Credentials>(entity =>
            {
                entity.ToTable("Credentials");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd() // Configures Id as auto-increment
                      .UseIdentityColumn();
                entity.Property(e => e.Username).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(255);
                entity.Property(e => e.LastModifedOn);
                entity.Property(e => e.LastModifiedBy);
                
            });
        }
    }
}
