using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Repositories.GenericRepository;

namespace CleanArchitecture.Infrastructure.Repositories.ProductRepository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        IQueryable<Product> OrderProduct(IQueryable<Product> queryable, string order, string orderDirection);
    }
}
