using CleanArchitecture.Infrastructure.Repositories.BrandRepository;
using CleanArchitecture.Infrastructure.Repositories.CartItemsRepository;
using CleanArchitecture.Infrastructure.Repositories.CartRepository;
using CleanArchitecture.Infrastructure.Repositories.CategoryRepository;
using CleanArchitecture.Infrastructure.Repositories.GenericRepository;
using CleanArchitecture.Infrastructure.Repositories.OrderItemsRepository;
using CleanArchitecture.Infrastructure.Repositories.OrderRepository;
using CleanArchitecture.Infrastructure.Repositories.ProductRepository;
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
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IBrandRepository, BrandRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICartRepository, CartRepository>();
            services.AddTransient<ICartItemsRepository, CartItemsRepository>();
            services.AddTransient<IOrderItemsRepository, OrderItemsRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();


            return services;
        }

    }
}
