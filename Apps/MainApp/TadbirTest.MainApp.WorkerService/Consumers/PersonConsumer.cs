using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TadbirTest.MainApp.Application.Persons.Commands;
using TadbirTest.MainApp.Domain;
using TadbirTest.MainApp.Domain.Entities.Persons;
using TadbirTest.MainApp.Infrastructure.Redis.Interfaces;
using TadbirTest.Shared;

namespace TadbirTest.MainApp.WorkerService.Consumers
{
    class PersonConsumer : IConsumer<PersonMessage>
    {
        ILogger<PersonConsumer> _logger;
        private readonly IMediator mediator;
        private readonly IDistrbutedCacheHelper distrbutedCache;

        public PersonConsumer(ILogger<PersonConsumer> logger,IMediator mediator, IDistrbutedCacheHelper distrbutedCache)
        {
            _logger = logger;
            this.mediator = mediator;
            this.distrbutedCache = distrbutedCache;
        }

        public async Task Consume(ConsumeContext<PersonMessage> context)
        {
            if (context.Message.FirstName == null)
                return;

            var personMessage = context.Message;
            var person = personMessage.ToPersonEntity();

            await AddPersonToSql(person);
            await AddPersonToRedis(person);
        }

        private async Task AddPersonToSql(Person person)
        {
            await mediator.Send(new AddPersonCommand(person));
        }
        
        private async Task AddPersonToRedis(Person person)
        {
            await distrbutedCache.SetAsync(person);
        }
    }
}
