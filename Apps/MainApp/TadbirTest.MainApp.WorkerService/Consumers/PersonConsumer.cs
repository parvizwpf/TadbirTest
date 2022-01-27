using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
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
        ILogger<PersonConsumer> logger;
        private readonly IMediator mediator;
        private readonly IDistributedCacheHelper distributedCache;

        public PersonConsumer(ILogger<PersonConsumer> logger,
            IMediator mediator,
            IDistributedCacheHelper distrbutedCache)
        {
            this.logger = logger;
            this.mediator = mediator;
            distributedCache = distrbutedCache;
        }

        public async Task Consume(ConsumeContext<PersonMessage> context)
        {
            var personMessage = context.Message;
            var person = personMessage.ToPersonEntity();

            CreatePersonLog(person);
            await AddPersonToSql(person);
            await AddPersonToRedis(person);
        }

        private void CreatePersonLog(Person person)
        {
            //log to => file & seq
            logger.LogInformation($"Received: Person > {person.FirstName} {person.LastName}");
        }

        private async Task AddPersonToSql(Person person)
        {
            await mediator.Send(new AddPersonCommand(person));
        }

        private async Task AddPersonToRedis(Person person)
        {
            var key = $"Person_{Guid.NewGuid()}";
            await distributedCache.SetAsync(key, person);
        }
    }
}
