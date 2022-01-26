using MassTransit;
using System;
using System.Threading;
using System.Threading.Tasks;
using TadbirTest.Shared;

namespace TadbirTest.ConsoleApp.RabbitMQ
{
    public class RabbitMQPublisher
    {
        public static async Task PublishPerson()
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq();
            var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            await busControl.StartAsync(source.Token);

            try
            {
                while (true)
                {
                    string value = await Task.Run(() =>
                    {
                        Console.WriteLine("Enter message (or quit to exit)");
                        Console.Write("> ");
                        return Console.ReadLine();
                    });

                    if ("quit".Equals(value, StringComparison.OrdinalIgnoreCase))
                        break;

                    await busControl.Publish<IPersonMessage>(new Person
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
