using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Repositories.GenericRepository;

namespace CleanArchitecture.Infrastructure.Repositories.OrderRepository
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
    }
}
