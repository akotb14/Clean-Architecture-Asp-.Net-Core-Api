using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Repositories.GenericRepository;

namespace CleanArchitecture.Infrastructure.Repositories.CourseRepository
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
    }
}
