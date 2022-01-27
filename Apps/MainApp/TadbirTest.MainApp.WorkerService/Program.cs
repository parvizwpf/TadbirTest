using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.IO;
using TadbirTest.MainApp.Application;
using TadbirTest.MainApp.Domain.Configs;
using TadbirTest.MainApp.Infrastructure;
using TadbirTest.MainApp.Persistence;
using TadbirTest.MainApp.WorkerService.Consumers;
using TadbirTest.MainApp.WorkerService.Settings;

namespace TadbirTest.MainApp.WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BaseConfig(args);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration configuration = hostContext.Configuration;
                    services.AddSingleton<IConnectionConfig, ConnectionConfig>();
                    services.AddApplicationServices();
                    services.AddInfrastructureServices();
                    services.AddPersistanceServices();

                    services.AddMassTransit(x =>
                    {
                        x.AddConsumer<PersonConsumer>();
                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.Host(new Uri(RabbitMqConsts.RabbitMqRootUri), h =>
                            {
                                h.Username(RabbitMqConsts.UserName);
                                h.Password(RabbitMqConsts.Password);
                            });
                            cfg.ReceiveEndpoint("testQueue", e =>
                            {
                                e.ConfigureConsumer<PersonConsumer>(context);
                            });
                        });
                    });
                    services.AddMassTransitHostedService();
                    services.AddHostedService<Worker>();
                })
                .UseSerilog((context, services, configuration) => configuration
                    .ReadFrom.Configuration(context.Configuration)
                );

        private static int BaseConfig(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .CreateLogger();

            try
            {
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Information(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }

    public class RabbitMqConsts
    {
        public const string RabbitMqRootUri = "rabbitmq://localhost";
        public const string RabbitMqUri = "rabbitmq://localhost/testQueue";
        public const string UserName = "guest";
        public const string Password = "guest";
    }
}
