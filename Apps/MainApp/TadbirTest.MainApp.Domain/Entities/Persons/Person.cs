using Dapper.Contrib.Extensions;

namespace TadbirTest.MainApp.Domain.Entities.Persons
{
    [Table("Person")]
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}
