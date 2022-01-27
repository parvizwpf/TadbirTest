using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TadbirTest.MainApp.Infrastructure.Redis;
using TadbirTest.MainApp.Infrastructure.Redis.Interfaces;
using TadbirTest.Shared;

namespace TadbirTest.MainApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var redisConfig = configuration.Get<BaseConfig>().RedisConfig;
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConfig.Host;
                options.InstanceName = "";
            });
            services.AddTransient<IDistributedCacheHelper, DistributedCacheHelper>();

            return services;
        }
    }
}
