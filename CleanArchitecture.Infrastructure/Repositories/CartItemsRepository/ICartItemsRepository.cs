using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Repositories.GenericRepository;

namespace CleanArchitecture.Infrastructure.Repositories.CartItemsRepository
{
    public interface ICartItemsRepository : IGenericRepository<CartItem>
    {
        Task<int> SumQuantity(int CartID);
    }
}
