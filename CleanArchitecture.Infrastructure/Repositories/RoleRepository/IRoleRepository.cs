using CleanArchitecture.Infrastructure.Repositories.GenericRepository;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Infrastructure.Repositories.RoleRepository
{
    public interface IRoleRepository : IGenericRepository<IdentityRole>
    {
        RoleManager<IdentityRole> GetRoleManager();
    }
}
