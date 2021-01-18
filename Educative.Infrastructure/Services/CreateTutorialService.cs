using System.Threading.Tasks;
using Educative.Domain.DTO;
using Educative.Domain.Exceptions;
using Educative.Domain.Entity;
using Educative.Domain.Repository;
using Educative.Domain.Services;

namespace Educative.Infrastructure.Services
{
    public class CreateTutorialService : ICreateTutorialService
    {
        private readonly ITutorialRepository repository;
        private readonly ICourseRepository courseRepository;
        private readonly IMediaObjectRepository mediaObjectRepository;

        public CreateTutorialService(ITutorialRepository repository, ICourseRepository courseRepository, IMediaObjectRepository mediaObjectRepository)
        {
            this.repository = repository;
            this.courseRepository = courseRepository;
            this.mediaObjectRepository = mediaObjectRepository;
        }

        public async Task<Tutorial> execute(TutorialRequest request)
        {
            if (!request.courseId.HasValue)
            {
                throw new InvalidArgumentException("courseId is required");
            }
            Course course = await courseRepository.GetById(request.courseId.Value);
            if (course == null)
            {
                throw new NotFoundException(string.Format("Course with id {0} is not found", request.courseId));
            }
            Tutorial tutorial = new Tutorial()
            {
                name = request.name,
                content = request.content,
                course = course
            };
            if (request.imageId.HasValue)
            {
                MediaObject media = await mediaObjectRepository.GetById(request.imageId.Value);
                if (media == null)
                {
                    throw new NotFoundException(string.Format("Image with id {0} is not found", request.imageId));
                }
                tutorial.image = media;
            }
            await repository.Add(tutorial);
            await repository.saveChanges();
            return tutorial;
        }
    }
}
