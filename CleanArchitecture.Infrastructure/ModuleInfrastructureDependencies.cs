using CleanArchitecture.Infrastructure.Repositories.CourseRepository;
using CleanArchitecture.Infrastructure.Repositories.GenericRepository;
using CleanArchitecture.Infrastructure.Repositories.RefreshTokenRepository;
using CleanArchitecture.Infrastructure.Repositories.RoleRepository;
using CleanArchitecture.Infrastructure.Repositories.UserRepository;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddModuleInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();

            return services;
        }

    }
}
