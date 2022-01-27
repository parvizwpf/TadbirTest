using TadbirTest.MainApp.Domain.Entities.Persons;
using TadbirTest.Shared;

namespace TadbirTest.MainApp.Domain
{
    public static class Extensions
    {
        public static Person ToPersonEntity(this PersonMessage message)
        {
            return new Person
            {
                Age = message.Age,
                FirstName = message.FirstName,
                LastName = message.LastName
            };
        }
    }
}
