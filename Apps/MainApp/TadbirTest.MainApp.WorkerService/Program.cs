using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TadbirTest.MainApp.Infrastructure;
using TadbirTest.MainApp.Persistence;

namespace TadbirTest.MainApp.WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddInfrastructureServices();
                    services.AddPersistanceServices();

                    services.AddHostedService<Worker>();
                });
    }
}
