using Kolisetka.Application.Persistence.Contracts;
using Kolisetka.Domain;

namespace Kolisetka.Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly KolisetkaDbContext _context;

        public ProductRepository(KolisetkaDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
