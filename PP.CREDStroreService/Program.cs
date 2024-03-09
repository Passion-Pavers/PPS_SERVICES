
using Microsoft.EntityFrameworkCore;
using PP.CREDStroreService.BusinessService;
using PP.CREDStroreService.BusinessService.Contract;
using PP.CREDStroreService.Data;
using PP.CREDStroreService.Models.DbEntities;
using PP.CREDStroreService.Repository;
using PP.CREDStroreService.Repository.Contract;

namespace PP.CREDStroreService
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
                   options.UseNpgsql(builder.Configuration.GetConnectionString("CredStoreConnectionString")));
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddScoped<ICredDataStoreRepo, CredDataStoreRepo>();
            builder.Services.AddScoped<ICredStoreBusinessService, CredStoreBusinessService>();
            builder.Services.AddHttpContextAccessor();



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
