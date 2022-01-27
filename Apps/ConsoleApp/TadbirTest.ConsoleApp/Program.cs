using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using TadbirTest.ConsoleApp.RabbitMQ;
using TadbirTest.Shared;

namespace TadbirTest.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var config = GetRabbitConfig();
            await RabbitMQPublisher.PublishPerson(config);
        }

        static RabbitMQConfig GetRabbitConfig()
        {
            var environmentName = Environment.GetEnvironmentVariable("ENVIRONMENT");
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", false)
               .AddJsonFile($"appsettings.{environmentName}.json", true)
               .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();
            var mySettingsConfig = configuration.Get<BaseConfig>();
            return mySettingsConfig.RabbitMQConfig;
        }
    }
}
