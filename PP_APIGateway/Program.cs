using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using PP_APIGateway.Extensions;

namespace PP_APIGateway
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            builder.AddAppAuthetication();
            builder.Services.AddOcelot(builder.Configuration);
            var app = builder.Build();
            await app.UseOcelot();

           

            app.Run();
        }
    }
}
