using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Repositories.GenericRepository;

namespace CleanArchitecture.Infrastructure.Repositories.CategoryRepository
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
    }
}
