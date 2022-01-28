using Microsoft.Extensions.Caching.Distributed;
using System.Threading.Tasks;
using TadbirTest.MainApp.Infrastructure.Redis.Interfaces;
using TadbirTest.Shared.Helpers;

namespace TadbirTest.MainApp.Infrastructure.Redis
{
    public class DistributedCacheHelper : IDistributedCacheHelper
    {
        private readonly IDistributedCache cache;

        public DistributedCacheHelper(IDistributedCache cache)
        {
            this.cache = cache;
        }

        public async Task SetAsync(string key, object obj)
        {
            try
            {
                var model = obj.ToByteArray();
                await cache.SetAsync(key, model);
            }
            catch
            {
                throw;
            }
        }
    }
}
