using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Context;
using CleanArchitecture.Infrastructure.Repositories.GenericRepository;

namespace CleanArchitecture.Infrastructure.Repositories.OrderItemsRepository
{
    public class OrderItemsRepository : GenericRepository<OrderItem>, IOrderItemsRepository
    {
        public OrderItemsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
