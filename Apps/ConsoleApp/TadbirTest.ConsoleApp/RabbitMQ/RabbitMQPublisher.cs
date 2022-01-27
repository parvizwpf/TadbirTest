using MassTransit;
using System;
using System.Threading;
using System.Threading.Tasks;
using TadbirTest.Shared;

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

            var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            await busControl.StartAsync(source.Token);

            try
            {
                while (true)
                {
                    //string value = await Task.Run(() =>
                    //{
                    //    Console.WriteLine("Enter message (or quit to exit)");
                    //    Console.Write("> ");
                    //    return Console.ReadLine();
                    //});

                    //if ("quit".Equals(value, StringComparison.OrdinalIgnoreCase))
                    //    break;

                    await busControl.Publish(new PersonMessage
                    {
                        FirstName = "Roli",
                        LastName = "Moli",
                        Age = 34
                    });
                }
            }
            finally
            {
                await busControl.StopAsync();
            }
        }
    }
}
