using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;
using System.Threading.Tasks;
using TadbirTest.MainApp.Domain.Entities.Persons;
using TadbirTest.MainApp.Domain.Repositories;

namespace TadbirTest.MainApp.Persistence.Repositories
{
    public class PersonRepository : Repository, IPersonRepository
    {
        public PersonRepository(IDbConnection dbConnection, IDbTransaction dbTransaction) :
            base(dbConnection, dbTransaction)
        {
        }

        public Task<int> Insert(Person person)
        {
            //using Dapper.Contrib
            return DbConnection.InsertAsync(person);
        }

        //public Task<int> Inserts(Person person)
        //{
        //    //using Dapper
        //    string query = "INSERT INTO [Person] (FirstName,LastName,Age) Values (@FirstName,@LastName,@Age);";

        //    return DbConnection.ExecuteAsync(query,
        //        new
        //        {
        //            FirstName = person.FirstName,
        //            LastName = person.FirstName,
        //            Age = person.Age
        //        });
        //}
    }
}
