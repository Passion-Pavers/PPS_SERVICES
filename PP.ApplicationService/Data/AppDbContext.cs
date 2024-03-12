using Microsoft.EntityFrameworkCore;
using PP.ApplicationService.Models.DbEntities;


namespace PP.ApplicationService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Application> Applications { get; set; }

        public DbSet<SubApplications> SubApps { get; set; }


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
                entity.Property(e => e.AppConfigJson).HasMaxLength(255);
                entity.Property(e => e.LastModifiedBy);
                entity.Property(e => e.LastModifiedOn);
            });

            modelBuilder.Entity<SubApplications>(entity =>
            {
                entity.ToTable("SubApplications");
                entity.HasKey(e => e.SubAppID);
                entity.Property(e => e.SubAppName).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Description).HasMaxLength(255);
                entity.Property(e => e.AppConfigJson).HasMaxLength(255);
                entity.Property(e => e.LastModifiedBy);
                entity.Property(e => e.LastModifiedOn);
                entity.HasOne(s => s.Application)
                    .WithMany(a => a.SubApps)
                    .HasForeignKey(s => s.AppID);


            });


            modelBuilder.Entity<AppDataBases>(entity =>
                {
                    entity.ToTable("AppDataBases");
                    entity.HasKey(e => e.DBId);
                    entity.Property(e => e.DBId)
                          .ValueGeneratedOnAdd() // Configures Id as auto-increment
                          .UseIdentityColumn();
                    entity.Property(e => e.DBName).IsRequired().HasMaxLength(255);

                });

            modelBuilder.Entity<AppDbMapping>(entity =>
            {
                entity.ToTable("AppDbMapping");
                entity.HasKey(e => e.AppDBId);
                entity.Property(e => e.AppDBId)
                      .ValueGeneratedOnAdd() // Configures Id as auto-increment
                      .UseIdentityColumn();
                entity.Property(e => e.DbId).IsRequired();
                entity.Property(e => e.AppId).IsRequired();


            });

            modelBuilder.Entity<AppConfiguration>(entity =>
            {
                entity.ToTable("AppConfiguration");
                entity.HasKey(e => e.AppConfigId);
                entity.Property(e => e.AppConfigId)
                      .ValueGeneratedOnAdd() // Configures Id as auto-increment
                      .UseIdentityColumn();
                entity.Property(e => e.AppId).IsRequired();
                entity.Property(e => e.AppConfigJson).HasMaxLength(255);


            });
        }
    }
}
