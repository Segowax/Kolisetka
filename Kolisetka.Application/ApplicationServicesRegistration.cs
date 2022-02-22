using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Kolisetka.Application
{
    public static class ApplicationServicesRegistration
    {
        public static void ConfigureApplicationServices(this IServiceCollection services)
        {
            // It register all automapper profiles!
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
