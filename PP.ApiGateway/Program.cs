using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace PP.ApiGateway
{
    public class Program
    {
        public static async void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
                    .AddJsonFile("ocelot.json",optional:false, reloadOnChange:true)
                    .AddEnvironmentVariables();

            builder.Services.AddOcelot(builder.Configuration);


            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");
            await app.UseOcelot();
            app.Run();
        }
    }
}
