using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TadbirTest.Shared;

namespace TadbirTest.MainApp.Infrastructure.RabbitMQ
{
    class PersonConsumer : IConsumer<IPersonMessage>
    {
        ILogger<PersonConsumer> _logger;

        public PersonConsumer(ILogger<PersonConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<IPersonMessage> context)
        {
            _logger.LogInformation("Person: {Value}", context.Message.Person.FirstName);
        }
    }
}
