using TadbirTest.MainApp.Domain.Repositories;

namespace TadbirTest.MainApp.Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        IPersonRepository PersonRepository { get; }
        void StartTransaction();
        void Commit();
    }
}
