using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Context;
using CleanArchitecture.Infrastructure.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories.CartRepository
{
    public class CartRepository : GenericRepository<ShoppingCart>, ICartRepository
    {
        private readonly DbSet<ShoppingCart> _shoppingCarts;
        public CartRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _shoppingCarts = dbContext.Set<ShoppingCart>();
        }


    }
}
