using System.Threading.Tasks;
using Educative.Domain.DTO;
using Educative.Domain.Exceptions;
using Educative.Domain.Entity;
using Educative.Domain.Repository;
using Educative.Domain.Services;

namespace Educative.Infrastructure.Services
{
    public class UpdateCourseService : IUpdateCourseService
    {
        private readonly ICourseRepository repository;
        private readonly ITrackRepository trackRepository;
        private readonly IMediaObjectRepository mediaObjectRepository;

        public UpdateCourseService(ICourseRepository repository, ITrackRepository trackRepository, IMediaObjectRepository mediaObjectRepository)
        {
            this.repository = repository;
            this.trackRepository = trackRepository;
            this.mediaObjectRepository = mediaObjectRepository;
        }
        public async Task<Course> execute(CourseRequest request, int id)
        {
            Course course = await repository.GetById(id);
            if (course == null)
            {
                throw new NotFoundException(string.Format("Course with id {0} is not found", id));
            }
            course.name = request.name != null ? request.name : course.name;
            course.description = request.description != null ? request.description : course.description;

            if (request.trackId.HasValue && request.trackId.Value != course.track.id)
            {
                Track track = await trackRepository.GetById(request.trackId.Value);
                if (track == null)
                {
                    throw new NotFoundException(string.Format("Track with id {0} is not found", request.trackId));
                }
                course.track = track;
            }
            if (request.imageId.HasValue && request.imageId.Value != course.image?.id)
            {
                MediaObject media = await mediaObjectRepository.GetById(request.imageId.Value);
                if (media == null)
                {
                    throw new NotFoundException(string.Format("Image with id {0} is not found", request.trackId));
                }
                course.image = media;
            }
            await repository.saveChanges();
            return course;
        }
    }
}
