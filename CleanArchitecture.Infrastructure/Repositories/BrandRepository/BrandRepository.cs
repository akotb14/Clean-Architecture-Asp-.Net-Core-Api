using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Context;
using CleanArchitecture.Infrastructure.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories.BrandRepository
{
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        private readonly DbSet<Brand> _brand;
        public BrandRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _brand = dbContext.Set<Brand>();
        }
    }
}
