using System.Threading.Tasks;
using TadbirTest.ConsoleApp.RabbitMQ;

namespace TadbirTest.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //IConfiguration Config = new ConfigurationBuilder()
            //    .AddJsonFile("appSettings.json")
            //    .Build();
            //var rabbitConfig = Config.GetSection("EventBusSettings:HostAddress");
            await RabbitMQPublisher.PublishPerson();
        }
    }
}
