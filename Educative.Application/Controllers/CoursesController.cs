using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Educative.Application.Utils;
using Educative.Domain.DTO;
using Educative.Domain.Entity;
using Educative.Domain.Repository;
using Educative.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Educative.Application.Controllers
{
    [Route("/api")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository repository;
        private readonly IMapper mapper;
        private readonly ICreateCourseService createCourseService;
        private readonly IUpdateCourseService updateCourseService;

        public CoursesController(ICourseRepository repository, IMapper mapper, ICreateCourseService createCourseService, IUpdateCourseService updateCourseService)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.createCourseService = createCourseService;
            this.updateCourseService = updateCourseService;
        }

        [HttpGet("courses")] [Produces("application/json")]
        public async Task<ActionResult<ICollection<CourseCollection>>> Index([FromQuery] int page = 1, [FromQuery] int perPage = 15)
        {

            ICollection<Course> courses = await repository.GetAll(new Paginator(page, perPage));
            ICollection<CourseCollection> output = mapper.Map<ICollection<CourseCollection>>(courses);
            var url =  courses.Count > perPage-1 ?  Url.Action("Index", new { page = page + 1, perPage }) : null;
            return Ok(new {data = output, next_page = url});
        }

        [HttpGet("courses/{id}")] [Produces("application/json")]
        public async Task<ActionResult<CourseDetails>> Show(int id)
        {
            Course course = await repository.GetById(id);
            if (course == null)
            {
                return NotFound();
            }
            CourseDetails output = mapper.Map<CourseDetails>(course);

            return Ok(output);
        }
        [HttpGet("tracks/{id}/courses")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<CourseDetails>>> GetCourses(int id)
        {
            IEnumerable<Course> courses = await repository.GetByTrack(id);
            IEnumerable<CourseDetails> output = mapper.Map<IEnumerable<CourseDetails>>(courses);
            return Ok(output);
        }
        [HttpPost("courses")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CourseDetails>> Store(CourseRequest input)
        {
            try
            {
                Course course = await createCourseService.execute(input);
                CourseDetails output = mapper.Map<CourseDetails>(course);
                return Created("", output);
            }
            catch (System.Exception e)
            {
                return ExceptionResultHandler.Handle(e);
            }
        }

        [HttpPatch("courses/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CourseDetails>> Update(CourseRequest input, int id)
        {
            try
            {
                Course course = await updateCourseService.execute(input, id);
                CourseDetails output = mapper.Map<CourseDetails>(course);
                return Ok(output);
            }
            catch (System.Exception e)
            {
                return ExceptionResultHandler.Handle(e);
            }
        }
        [HttpDelete("courses/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Destroy(int id)
        {
            Course course = await repository.GetById(id);
            if (course != null)
            {
                repository.Delete(course);
                await repository.saveChanges();
            }
            return NoContent();
        }
    }
}
