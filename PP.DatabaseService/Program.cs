
using Microsoft.EntityFrameworkCore;
using PP.DatabaseService.Data;
using PP.DatabaseService.Repository;
using PP.DatabaseService.Repository.Contract;

namespace PP.DatabaseService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(options =>
                 options.UseNpgsql(builder.Configuration.GetConnectionString("ApplicationConnectionString")));

            builder.Services.AddScoped<IDBDataRepo, DBDataRepo>(); 
                builder.Services.AddScoped<IDataBaseService, DataBaseService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
