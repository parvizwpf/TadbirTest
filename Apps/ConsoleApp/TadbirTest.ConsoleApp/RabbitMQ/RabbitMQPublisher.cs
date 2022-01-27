using MassTransit;
using System;
using System.Threading;
using System.Threading.Tasks;
using TadbirTest.Shared;
using TadbirTest.Shared.Helpers;

namespace TadbirTest.ConsoleApp.RabbitMQ
{
    public class RabbitMQPublisher
    {
        public static async Task PublishPerson(RabbitMQConfig rabbitConfig)
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(config =>
            {
                config.Host(new Uri(rabbitConfig.RabbitMqRootUri), h =>
                {
                    h.Username(rabbitConfig.UserName);
                    h.Password(rabbitConfig.Password);
                });
            });

            long counter = 1;
            var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            await busControl.StartAsync(source.Token);

            try
            {
                while (true)
                {
                    var person = PersonMessageHelper.GetPersonMessage();
                    Console.WriteLine($"{counter} > Published Person [{person.FirstName} {person.LastName}]");
                    await busControl.Publish(person);
                    counter++;
                    await Task.Delay(3000);
                }
            }
            finally
            {
                await busControl.StopAsync();
            }
        }
    }
}
