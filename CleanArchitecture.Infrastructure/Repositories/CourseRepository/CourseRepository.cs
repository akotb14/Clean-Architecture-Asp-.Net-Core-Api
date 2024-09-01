using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Context;
using CleanArchitecture.Infrastructure.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories.CourseRepository
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        private readonly DbSet<Course> _courses;
        public CourseRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _courses = dbContext.Set<Course>();
        }
    }
}
