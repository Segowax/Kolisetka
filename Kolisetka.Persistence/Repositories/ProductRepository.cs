using Kolisetka.Application.Contracts.Persistence;
using Kolisetka.Domain;

namespace Kolisetka.Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(KolisetkaDbContext context) : base(context) { }
    }
}
