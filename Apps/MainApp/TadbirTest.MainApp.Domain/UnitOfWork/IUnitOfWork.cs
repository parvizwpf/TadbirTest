using TadbirTest.MainApp.Domain.Repositories;

namespace TadbirTest.MainApp.Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        IPersonRepository IPersonRepository { get; }
        void StartTransaction();
        void Commit();
    }
}
