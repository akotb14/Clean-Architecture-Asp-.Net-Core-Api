using CleanArchitecture.Domain.Entities.Identity;

namespace CleanArchitecture.Application.Services.CurrentUserService
{
    public interface ICurrentUserService
    {
        public Task<User> GetUserAsync();
        public string GetUserId();
        public Task<List<string>> GetCurrentUserRolesAsync();

    }
}
