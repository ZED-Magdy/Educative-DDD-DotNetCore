using System.Threading.Tasks;
using Educative.Domain.DTO;
using Educative.Domain.Entity;

namespace Educative.Domain.Services
{
    public interface IUpdateTrackService
    {
        Task<Track> execute(TrackRequest request, int id);
    }
}
