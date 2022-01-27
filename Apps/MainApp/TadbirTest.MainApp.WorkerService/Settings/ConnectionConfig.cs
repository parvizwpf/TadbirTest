using Microsoft.Extensions.Configuration;
using TadbirTest.MainApp.Domain.Configs;

namespace TadbirTest.MainApp.WorkerService.Settings
{
    public class ConnectionConfig : IConnectionConfig
    {
        public IConfiguration Configuration { get; }
        public ConnectionConfig(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public string SqlConnection => Configuration.GetConnectionString("SqlConnection");
    }
}
