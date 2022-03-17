using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Kolisetka.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            // It register all automapper profiles at once!
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            // MediatR
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
