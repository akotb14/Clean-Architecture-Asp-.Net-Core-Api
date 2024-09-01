using CleanArchitecture.Application.ResultHandler;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Extensions
{
    public static class PaginatedListExtension
    {
        public static async Task<PaginatedResult<T>> ToPaginatedListAsync<T>(this IQueryable<T> queryable, int pageNumber, int pageSize)
             where T : class
        {
            if (queryable == null) throw new Exception("Empty");
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize == 0 ? 10 : pageSize;
            int count = await queryable.AsNoTracking().CountAsync();
            // if (count == 0) return PaginatedResponse<T>.Success(new List<T>(), count, pageNumber, pageSize);
            if (count == 0) return new PaginatedResult<T>()
            {
                Data = new List<T>(),
                Succeeded = true,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                Messages = "success"
            };
            var items = await queryable.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            // return PaginatedResponse<T>.Success(items, count, pageNumber, pageSize);
            return new PaginatedResult<T>()
            {
                Data = items,
                Succeeded = true,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                Messages = "success"
            };
        }
    }
}
