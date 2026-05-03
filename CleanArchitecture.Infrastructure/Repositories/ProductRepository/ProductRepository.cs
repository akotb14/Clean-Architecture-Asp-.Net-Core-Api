using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Infrastructure.Context;
using CleanArchitecture.Infrastructure.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CleanArchitecture.Infrastructure.Repositories.ProductRepository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly DbSet<Product> _product;
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _product = dbContext.Set<Product>();
        }

        private ProductOrderEnum? ParseProductOrderEnum(string orderBy)
        {
            if (Enum.TryParse(orderBy, true, out ProductOrderEnum orderEnum))
            {
                return orderEnum;
            }
            return null;
        }
        private OrderDirectionEnum? ParseUserOrderDirectionEnum(string orderBy)
        {
            if (Enum.TryParse(orderBy, true, out OrderDirectionEnum userOrderDirectionEnum))
            {
                return userOrderDirectionEnum;
            }
            return null;
        }
        public IQueryable<Product> OrderProduct(IQueryable<Product> queryable, string order, string orderDirection)
        {
            ProductOrderEnum? orderEnum = ParseProductOrderEnum(order);
            OrderDirectionEnum? directionEnum = ParseUserOrderDirectionEnum(orderDirection);
            if (orderEnum == null || directionEnum == null)
                return queryable;

            var orderMappings = new Dictionary<ProductOrderEnum, Expression<Func<Product, object>>>
            {
                    { ProductOrderEnum.Name, e => e.Name },
                    { ProductOrderEnum.Description, e => e.Description },
                    { ProductOrderEnum.CreatedDate, e => e.CreatedDate },
                    { ProductOrderEnum.Price, e => e.Price },
                    { ProductOrderEnum.ModifiedDate, e => e.ModifiedDate },
                    { ProductOrderEnum.CategoryID, e => e.CategoryID },
                    { ProductOrderEnum.BrandID, e => e.BrandID },
                    { ProductOrderEnum.StockQuantity, e => e.StockQuantity }
            };

            if (orderMappings.TryGetValue(orderEnum.Value, out var orderExpression))
            {
                queryable = directionEnum == OrderDirectionEnum.Ascending
                    ? queryable.OrderBy(orderExpression)
                    : queryable.OrderByDescending(orderExpression);
            }

            return queryable;
        }
    }
}
