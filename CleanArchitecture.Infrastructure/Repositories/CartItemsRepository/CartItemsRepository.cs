using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Context;
using CleanArchitecture.Infrastructure.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories.CartItemsRepository
{
    public class CartItemsRepository : GenericRepository<CartItem>, ICartItemsRepository
    {
        private readonly DbSet<CartItem> _cartItems;
        public CartItemsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _cartItems = dbContext.Set<CartItem>();
        }

        public async Task<int> SumQuantity(int CartID)
        {
            return await _cartItems.Where(e => e.CartID == CartID).SumAsync(x => x.Quantity);


        }
    }
}
