using System.Collections.Generic;
using System.Threading.Tasks;
using Educative.Domain.Entity;

namespace Educative.Domain.Repository
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<IEnumerable<Course>> GetByTrack(int trackId);
    }
}
