using MS.Libs.Infra.IoC.Extensions;
using MS.Services.Courses.WebAPI.Infrastructure;
using Serilog;
using Serilog.Events;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
             .ConfigureAppConfiguration((hostingContext, config) =>
             {
                 hostingContext.RegisterSerilog(config);
             })
            .UseSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                webBuilder.UseIISIntegration();
            });
}