using CleanArchitecture.Infrastructure.Context;
using CleanArchitecture.Infrastructure.Repositories.GenericRepository;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Infrastructure.Repositories.RoleRepository
{
    public class RoleRepository : GenericRepository<IdentityRole>, IRoleRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleRepository(ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager) : base(dbContext)
        {
            _roleManager = roleManager;
        }

        public RoleManager<IdentityRole> GetRoleManager()
        {
            return _roleManager;
        }
    }
}
