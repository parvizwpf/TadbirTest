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

        public async Task Insert(Person person)
        {
            await DbConnection.InsertAsync(person);
        }
    }
}
