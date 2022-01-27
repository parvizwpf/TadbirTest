using System.Data;
using TadbirTest.MainApp.Domain.Repositories;
using TadbirTest.MainApp.Domain.UnitOfWork;
using TadbirTest.MainApp.Persistence.Repositories;

namespace TadbirTest.MainApp.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection dbConnection;
        private IDbTransaction transaction;
        public UnitOfWork(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
            ManageConnection();
        }

        public IPersonRepository IPersonRepository => new PersonRepository(this.dbConnection, this.transaction);

        public void StartTransaction()
        {
            if (transaction == null)
            {
                ManageConnection();
                transaction = dbConnection.BeginTransaction();
            }
        }
        public void Commit()
        {
            try
            {
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
        }

        private void ManageConnection()
        {
            if (dbConnection.State == ConnectionState.Closed)
                dbConnection.Open();
        }
    }
}
