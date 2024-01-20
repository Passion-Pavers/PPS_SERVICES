
using Microsoft.EntityFrameworkCore;
using PP.Services.Data;
using System.Configuration;
using PP.Services.Controllers;

namespace PP.Services
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
            builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddSwaggerGen();
            builder.Services.AddCors();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors(options =>            
                options.WithOrigins("http://localhost")
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowAnyMethod()
            );

            app.UseAuthorization();


            app.MapControllers();


            app.Run();
        }
    }
}
