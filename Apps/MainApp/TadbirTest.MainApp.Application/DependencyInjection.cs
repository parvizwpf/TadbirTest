using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace TadbirTest.MainApp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(assembly);
            return services;
        }
    }
}
