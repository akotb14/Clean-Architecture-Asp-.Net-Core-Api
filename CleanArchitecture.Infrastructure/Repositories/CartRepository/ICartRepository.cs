using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Repositories.GenericRepository;

namespace CleanArchitecture.Infrastructure.Repositories.CartRepository
{
    public interface ICartRepository : IGenericRepository<ShoppingCart>
    {

    }
}
