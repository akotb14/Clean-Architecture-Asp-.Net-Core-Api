using CleanArchitecture.Domain.Entities.Identity;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Infrastructure.Context;
using CleanArchitecture.Infrastructure.Repositories.GenericRepository;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Infrastructure.Repositories.UserRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly UserManager<User> _userManager;
        public UserRepository(ApplicationDbContext dbContext, UserManager<User> userManager) : base(dbContext)
        {
            _userManager = userManager;
        }
        public UserManager<User> GetUserManager()
        {
            return _userManager;
        }

        private UserOrderEnum? ParseUserOrderEnum(string orderBy)
        {
            if (Enum.TryParse(orderBy, true, out UserOrderEnum orderEnum))
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
        public IQueryable<User> OrderUser(IQueryable<User> queryable, string order, string orderDirection)
        {
            UserOrderEnum? orderEnum = ParseUserOrderEnum(order);
            OrderDirectionEnum? directionEnum = ParseUserOrderDirectionEnum(orderDirection);
            switch (orderEnum)
            {
                case UserOrderEnum.UserName:
                    if (OrderDirectionEnum.Ascending == directionEnum)
                        queryable = queryable.OrderBy(e => e.UserName);
                    else
                        queryable = queryable.OrderByDescending(e => e.UserName);
                    break;
                case UserOrderEnum.FullName:
                    if (OrderDirectionEnum.Ascending == directionEnum)
                        queryable = queryable.OrderBy(e => e.FullName);
                    else
                        queryable = queryable.OrderByDescending(e => e.FullName);
                    break;
                case UserOrderEnum.Email:
                    if (OrderDirectionEnum.Ascending == directionEnum)
                        queryable = queryable.OrderBy(e => e.Email);
                    else
                        queryable = queryable.OrderByDescending(e => e.Email);
                    break;
                case UserOrderEnum.PhoneNumber:
                    if (OrderDirectionEnum.Ascending == directionEnum)
                        queryable = queryable.OrderBy(e => e.PhoneNumber);
                    else
                        queryable = queryable.OrderByDescending(e => e.PhoneNumber);
                    break;
                case UserOrderEnum.Address:
                    if (OrderDirectionEnum.Ascending == directionEnum)
                        queryable = queryable.OrderBy(e => e.Address);
                    else
                        queryable = queryable.OrderByDescending(e => e.Address);
                    break;
                default:
                    return queryable;

            }
            return queryable;
        }


    }
}
