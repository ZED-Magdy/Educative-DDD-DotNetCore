using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;
using Educative.Domain.Repository;
using Educative.Domain.Services;
using Educative.Domain.DTO;
using Educative.Domain.Entity;
using Educative.Application.Utils;

namespace Educative.Controllers
{
    [Route("api/tutorials")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class TutorialsController : ControllerBase
    {
        private readonly ITutorialRepository repository;
        private readonly IMapper mapper;
        private readonly ICreateTutorialService createTutorialService;
        private readonly IUpdateTutorialService updateTutorialService;

        public TutorialsController(ITutorialRepository repository, IMapper mapper, ICreateTutorialService createTutorialService, IUpdateTutorialService updateTutorialService)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.createTutorialService = createTutorialService;
            this.updateTutorialService = updateTutorialService;
        }

        [HttpGet] [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<TutorialCollection>>> Get([FromQuery]int page = 1, [FromQuery] int perPage = 10)
        {
            Paginator paginator = new Paginator(page, perPage);
            IEnumerable<Tutorial> tutorials = await repository.GetAll(paginator);
            IEnumerable<TutorialCollection> output = mapper.Map<IEnumerable<TutorialCollection>>(tutorials);
            return Ok(output);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<TutorialDetails>> Show(int id)
        {
            Tutorial tutorial = await repository.GetById(id);
            TutorialDetails output = mapper.Map<TutorialDetails>(tutorial);
            return Ok(output);
        }
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TutorialDetails>> Store(TutorialRequest input)
        {
            try
            {
                Tutorial tutorial = await createTutorialService.execute(input);
                return Created("", mapper.Map<TutorialDetails>(tutorial));
            }
            catch (System.Exception e)
            {
                return ExceptionResultHandler.Handle(e);
            }
        }
        [HttpPatch("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TutorialDetails>> Update(TutorialRequest input, int id)
        {
            try
            {
                Tutorial tutorial = await updateTutorialService.execute(input, id);
                return Ok(mapper.Map<TutorialDetails>(tutorial));
            }
            catch (System.Exception e)
            {
                return ExceptionResultHandler.Handle(e);
            }
        }

        [HttpDelete("{id}")] [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Destroy(int id)
        {
            Tutorial tutorial = await repository.GetById(id);
            repository.Delete(tutorial);
            await repository.saveChanges();
            return NoContent();
        }
    }
}
