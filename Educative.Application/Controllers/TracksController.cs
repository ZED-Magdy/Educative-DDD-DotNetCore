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
namespace Educative.Controllers
{
    [Route("/api/tracks")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class TracksController : ControllerBase
    {

        private readonly ITrackRepository repository;
        private readonly IMapper mapper;
        private readonly ICreateTrackService createTrackService;
        private readonly IUpdateTrackService updateTrackService;

        public TracksController(ITrackRepository repository, IMapper mapper, ICreateTrackService createTrackService, IUpdateTrackService updateTrackService)
        {
            this.mapper = mapper;
            this.createTrackService = createTrackService;
            this.updateTrackService = updateTrackService;
            this.repository = repository;
        }

        [Produces("application/json")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrackCollection>>> Get([FromQuery] int page = 1, [FromQuery] int perPage = 10)
        {
            var paginator = new Paginator(page, perPage);
            IEnumerable<Track> tracks = await repository.GetAll(paginator);
            IEnumerable<TrackCollection> TrackCollections = mapper.Map<IEnumerable<TrackCollection>>(tracks);

            return Ok(TrackCollections);
        }

        [HttpGet("{id}")][Produces("application/json")]
        public async Task<ActionResult<TrackDetails>> Show(int id)
        {
            Track track = await repository.GetById(id);
            if (track == null)
            {
                return NotFound();
            }
            TrackDetails TrackCollection = mapper.Map<TrackDetails>(track);

            return Ok(TrackCollection);
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TrackDetails>> Store(TrackRequest input)
        {
            try
            {
                Track track = await createTrackService.execute(input);
                TrackDetails TrackCollection = mapper.Map<TrackDetails>(track);

                return Created("", TrackCollection);
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
        public async Task<ActionResult<TrackDetails>> Update(TrackRequest input, int id)
        {
            try
            {
                Track track = await updateTrackService.execute(input, id);

                return Ok(mapper.Map<TrackDetails>(track));
            }
            catch (System.Exception e)
            {

                return ExceptionResultHandler.Handle(e);
            }
        }

        [HttpDelete("{id}")][ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Destroy(int id)
        {
            Track track = await repository.GetById(id);
            if (track == null)
            {
                return NotFound();
            }
            repository.Delete(track);
            await repository.saveChanges();
            return NoContent();
        }
    }
}
