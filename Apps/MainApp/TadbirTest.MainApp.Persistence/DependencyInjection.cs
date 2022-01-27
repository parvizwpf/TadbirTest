using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;
using TadbirTest.MainApp.Domain.Configs;
using TadbirTest.MainApp.Domain.UnitOfWork;

namespace TadbirTest.MainApp.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services)
        {
            services.AddSingleton<IDbConnection>(conn =>
                new SqlConnection(conn.GetService<IConnectionConfig>().SqlConnection));

            services.AddSingleton<IUnitOfWork>(uof =>
            new UnitOfWork.UnitOfWork(uof.GetService<IDbConnection>()));

            return services;
        }
    }
}
