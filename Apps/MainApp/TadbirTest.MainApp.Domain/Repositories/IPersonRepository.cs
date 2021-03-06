using System.Threading.Tasks;
using TadbirTest.MainApp.Domain.Entities.Persons;

namespace TadbirTest.MainApp.Domain.Repositories
{
    public interface IPersonRepository
    {
        Task<int> Insert(Person person);
    }
}
