using System.Threading.Tasks;

namespace TadbirTest.MainApp.Infrastructure.Redis.Interfaces
{
    public interface IDistrbutedCacheHelper
    {
        Task SetAsync(object obj);
    }
}
