using Kolisetka.Application.Contracts.Persistence;
using Kolisetka.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kolisetka.Persistence
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<KolisetkaDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("KolisetkaConnectionString")));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
