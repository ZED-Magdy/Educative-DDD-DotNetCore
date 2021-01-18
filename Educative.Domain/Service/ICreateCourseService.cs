using System.Threading.Tasks;
using Educative.Domain.DTO;
using Educative.Domain.Entity;

namespace Educative.Domain.Services
{
    public interface ICreateCourseService
    {
        Task<Course> execute(CourseRequest request);
    }
}
