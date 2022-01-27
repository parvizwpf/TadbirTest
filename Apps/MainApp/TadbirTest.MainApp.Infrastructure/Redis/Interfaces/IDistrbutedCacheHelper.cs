using System.Threading.Tasks;

namespace TadbirTest.MainApp.Infrastructure.Redis.Interfaces
{
    public interface IDistributedCacheHelper
    {
        Task SetAsync(string key, object obj);
    }
}
