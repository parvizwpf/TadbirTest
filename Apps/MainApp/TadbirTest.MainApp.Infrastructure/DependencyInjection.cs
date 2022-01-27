using Microsoft.Extensions.DependencyInjection;
using TadbirTest.MainApp.Infrastructure.Redis;
using TadbirTest.MainApp.Infrastructure.Redis.Interfaces;

namespace TadbirTest.MainApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:6379,abortConnect=false,syncTimeout=100000,asyncTimeout=100000";
                options.InstanceName = "";
            });

            services.AddTransient<IDistrbutedCacheHelper, DistributedCacheHelper>();

            return services;
        }
    }
}
