using System.Threading.Tasks;
using Educative.Domain.DTO;
using Educative.Domain.Exceptions;
using Educative.Domain.Entity;
using Educative.Domain.Repository;
using Educative.Domain.Services;

namespace Educative.Infrastructure.Services
{
    public class UpdateTrackService : IUpdateTrackService
    {
        private readonly ITrackRepository repository;
        private readonly IMediaObjectRepository mediaObjectRepository;

        public UpdateTrackService(ITrackRepository repository, IMediaObjectRepository mediaObjectRepository)
        {
            this.repository = repository;
            this.mediaObjectRepository = mediaObjectRepository;
        }
        public async Task<Track> execute(TrackRequest request, int id)
        {
            Track track = await repository.GetById(id);
            if (track == null)
            {
                throw new NotFoundException(string.Format("Track with id {0} is not found", id));
            }
            track.name = request.name != null ? request.name : track.name;
            track.description = request.description != null ? request.description : track.description;

            if (request.imageId.HasValue && request.imageId.Value != track.image?.id)
            {
                MediaObject media = await mediaObjectRepository.GetById(request.imageId.Value);
                if (media == null)
                {
                    throw new NotFoundException(string.Format("Image with id {0} is not found", request.imageId));
                }
                track.image = media;
            }

            await repository.saveChanges();
            return track;
        }
    }
}
