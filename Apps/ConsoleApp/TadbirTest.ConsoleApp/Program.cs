using System.Threading.Tasks;
using TadbirTest.ConsoleApp.RabbitMQ;

namespace TadbirTest.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await RabbitMQPublisher.PublishPerson();
        }
    }
}
