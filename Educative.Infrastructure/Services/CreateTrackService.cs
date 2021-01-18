using System.Threading.Tasks;
using Educative.Domain.DTO;
using Educative.Domain.Exceptions;
using Educative.Domain.Entity;
using Educative.Domain.Repository;
using Educative.Domain.Services;

namespace Educative.Infrastructure.Services
{
    public class CreateTrackService : ICreateTrackService
    {
        private readonly ITrackRepository repository;
        private readonly IMediaObjectRepository mediaObjectRepository;

        public CreateTrackService(ITrackRepository repository, IMediaObjectRepository mediaObjectRepository)
        {
            this.repository = repository;
            this.mediaObjectRepository = mediaObjectRepository;
        }
        public async Task<Track> execute(TrackRequest request)
        {
            Track track = new Track
            {
                name = request.name,
                description = request.description
            };
            if (request.imageId.HasValue)
            {
                MediaObject media = await mediaObjectRepository.GetById(request.imageId.Value);
                if (media == null)
                {
                    throw new NotFoundException(string.Format("Image with id {0} is not found", request.imageId));
                }
                track.image = media;
            }
            await repository.Add(track);
            await repository.saveChanges();
            return track;
        }
    }
}
