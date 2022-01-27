using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TadbirTest.MainApp.Infrastructure;
using TadbirTest.MainApp.Persistence;
using MassTransit;
using Microsoft.Extensions.Configuration;
using TadbirTest.MainApp.WorkerService.Consumers;
using TadbirTest.MainApp.Application;
using TadbirTest.MainApp.Domain.Configs;
using TadbirTest.MainApp.WorkerService.Settings;
using System;

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
                });
    }

    public class RabbitMqConsts
    {
        public const string RabbitMqRootUri = "rabbitmq://localhost";
        public const string RabbitMqUri = "rabbitmq://localhost/testQueue";
        public const string UserName = "guest";
        public const string Password = "guest";
    }
}
