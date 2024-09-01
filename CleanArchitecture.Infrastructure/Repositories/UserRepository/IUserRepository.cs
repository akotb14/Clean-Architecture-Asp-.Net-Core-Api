using CleanArchitecture.Domain.Entities.Identity;
using CleanArchitecture.Infrastructure.Repositories.GenericRepository;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Infrastructure.Repositories.UserRepository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        UserManager<User> GetUserManager();
        IQueryable<User> OrderUser(IQueryable<User> queryable, string order, string orderDirection);
    }
}
