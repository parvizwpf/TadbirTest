using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TadbirTest.MainApp.Infrastructure.Redis;
using TadbirTest.MainApp.Infrastructure.Redis.Interfaces;
using TadbirTest.Shared;

namespace TadbirTest.MainApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:6379,abortConnect=false,syncTimeout=100000,asyncTimeout=100000";
                options.InstanceName = "";
            });

            services.AddMassTransit(x =>
            {
                x.AddConsumer<PersonConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ReceiveEndpoint("event-listener", e =>
                    {
                        e.ConfigureConsumer<PersonConsumer>(context);
                    });
                });
            });
            services.AddMassTransitHostedService();

            services.AddTransient<IDistrbutedCacheHelper, DistributedCacheHelper>();

            return services;
        }
    }

    class PersonConsumer : IConsumer<IPersonMessage>
    {
        ILogger<PersonConsumer> _logger;

        public PersonConsumer(ILogger<PersonConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<IPersonMessage> context)
        {
            _logger.LogInformation("Value: {Value}", context.Message.Person.FirstName);
        }
    }
}
