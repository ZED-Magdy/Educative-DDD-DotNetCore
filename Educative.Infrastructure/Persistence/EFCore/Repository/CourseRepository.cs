using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educative.Domain.Entity;
using Educative.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Educative.Infrastructure.Persistence.EFCore.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DBContext context;

        public CourseRepository(DBContext context)
        {
            this.context = context;
        }
        public async Task<ICollection<Course>> GetAll(Paginator paginator)
        {
            return await context.Courses.Include(c => c.tutorials)
            .Skip(paginator.skip)
            .Take(paginator.take)
            .ToListAsync();
        }
        public async Task<Course> GetById(int id)
        {
            return await context.Courses.Include(c => c.track).Include(c => c.tutorials).Include(c => c.image).FirstOrDefaultAsync(c => c.id == id);
        }
        public async Task<IEnumerable<Course>> GetByTrack(int trackId)
        {
            return await context.Courses.Where(c => c.track.id == trackId).Include(c => c.image).Include(c => c.track).ToListAsync();
        }
        public async Task Add(Course course)
        {
            await context.Courses.AddAsync(course);
        }
        public void Delete(Course course)
        {
            context.Courses.Remove(course);
        }
        public async Task saveChanges()
        {
            await context.SaveChangesAsync();
        }
    }
}
