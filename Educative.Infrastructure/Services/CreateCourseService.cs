using System.Threading.Tasks;
using Educative.Domain.DTO;
using Educative.Domain.Exceptions;
using Educative.Domain.Entity;
using Educative.Domain.Repository;
using Educative.Domain.Services;

namespace Educative.Infrastructure.Services
{
    public class CreateCourseService : ICreateCourseService
    {
        private readonly ICourseRepository repository;
        private readonly ITrackRepository trackRepository;
        private readonly IMediaObjectRepository mediaObjectRepository;

        public CreateCourseService(ICourseRepository repository, ITrackRepository trackRepository, IMediaObjectRepository mediaObjectRepository)
        {
            this.repository = repository;
            this.trackRepository = trackRepository;
            this.mediaObjectRepository = mediaObjectRepository;
        }

        public async Task<Course> execute(CourseRequest request)
        {
            if (!request.trackId.HasValue || !request.imageId.HasValue)
            {
                throw new InvalidArgumentException("All fields are required!");
            }
            Track track = await trackRepository.GetById(request.trackId.Value);
            if (track == null)
            {
                throw new NotFoundException(string.Format("Track with id {0} is not found", request.trackId));
            }
            MediaObject media = await mediaObjectRepository.GetById(request.imageId.Value);
            if (media == null)
            {
                throw new NotFoundException(string.Format("Image with id {0} is not found", request.imageId));
            }
            Course course = new Course
            {
                name = request.name,
                description = request.description,
                track = track,
                image = media
            };
            await repository.Add(course);
            await repository.saveChanges();
            return course;
        }
    }
}
