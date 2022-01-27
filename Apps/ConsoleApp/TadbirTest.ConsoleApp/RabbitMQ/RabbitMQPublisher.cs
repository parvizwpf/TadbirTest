using MassTransit;
using System;
using System.Threading;
using System.Threading.Tasks;
using TadbirTest.Shared.Helpers;

namespace TadbirTest.ConsoleApp.RabbitMQ
{
    public class RabbitMQPublisher
    {
        public class RabbitMqConsts
        {
            public const string RabbitMqRootUri = "rabbitmq://localhost";
            public const string RabbitMqUri = "rabbitmq://localhost/testQueue";
            public const string UserName = "guest";
            public const string Password = "guest";
        }

        public static async Task PublishPerson()
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(config =>
            {
                config.Host(new Uri(RabbitMqConsts.RabbitMqRootUri), h =>
                {
                    h.Username(RabbitMqConsts.UserName);
                    h.Password(RabbitMqConsts.Password);
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
