using System.Threading.Tasks;
using Educative.Domain.DTO;
using Educative.Domain.Exceptions;
using Educative.Domain.Entity;
using Educative.Domain.Repository;
using Educative.Domain.Services;

namespace Educative.Infrastructure.Services
{
    public class UpdateTutorialService : IUpdateTutorialService
    {
        private readonly ITutorialRepository repository;
        private readonly ICourseRepository courseRepository;
        private readonly IMediaObjectRepository mediaObjectRepository;

        public UpdateTutorialService(ITutorialRepository repository, ICourseRepository courseRepository, IMediaObjectRepository mediaObjectRepository)
        {
            this.repository = repository;
            this.courseRepository = courseRepository;
            this.mediaObjectRepository = mediaObjectRepository;
        }

        public async Task<Tutorial> execute(TutorialRequest request, int id)
        {
            Tutorial tutorial = await repository.GetById(id);
            if (tutorial == null)
            {
                throw new NotFoundException(string.Format("Tutorial with id {0} is not found", id));
            }
            tutorial.name = request.name != null ? request.name : tutorial.name;
            tutorial.content = request.content != null ? request.content : tutorial.content;
            if (request.courseId.HasValue && request.courseId.Value != tutorial.course.id)
            {
                Course course = await courseRepository.GetById(request.courseId.Value);
                if (course == null)
                {
                    throw new InvalidArgumentException(string.Format("Course with id {0} is not found", request.courseId));
                }
                tutorial.course = course;
            }
            if (request.imageId.HasValue && request.imageId.Value != tutorial.image?.id)
            {
                MediaObject media = await mediaObjectRepository.GetById(request.imageId.Value);
                if (media == null)
                {
                    throw new NotFoundException(string.Format("Image with id {0} is not found", request.imageId));
                }
                tutorial.image = media;
            }
            await repository.saveChanges();
            return tutorial;
        }
    }
}
