using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Context;
using CleanArchitecture.Infrastructure.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories.CategoryRepository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly DbSet<Category> _categories;
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _categories = dbContext.Set<Category>();
        }
    }
}
