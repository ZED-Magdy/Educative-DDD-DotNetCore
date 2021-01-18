using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Educative.Domain.Repository;
using Educative.Domain.Entity;

namespace Educative.Controllers
{
    [Route("/api/media_objects")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class MediaObjectsController : ControllerBase
    {
        private readonly IMediaObjectRepository repository;

        public MediaObjectsController(IMediaObjectRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MediaObject>>> Index([FromQuery] int page = 1, [FromQuery] int perPage = 10)
        {
            var paginator = new Paginator(page, perPage);
            return Ok(await repository.GetAll(paginator));
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MediaObject>> Show(int id)
        {
            MediaObject media = await repository.GetById(id);
            if (media == null)
            {
                return NotFound("Not found");
            }
            return Ok(media);
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MediaObject>> Store(IFormFile file)
        {
            if (file == null)
            {
                return BadRequest();
            }
            try
            {
                string filePath = await UploadFile(file);
                MediaObject media = new MediaObject() { path = filePath };
                await repository.Add(media);
                await repository.saveChanges();

                return Created("", media);
            }
            catch (System.Exception)
            {

                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string),StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Destroy(int id)
        {
            MediaObject media = await repository.GetById(id);
            if (media == null)
            {
                return NotFound("Not found");
            }
            repository.Delete(media);
            await repository.saveChanges();
            return NoContent();
        }
        private static async Task<string> UploadFile(IFormFile file)
        {
            string imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources/Images");
            if (!Directory.Exists(imagesPath))
            {
                Directory.CreateDirectory(imagesPath);
            }
            string extension = extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            string fileName = DateTime.Now.Ticks + extension;
            string filePath = Path.Combine(imagesPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return "Resources/Images/" + fileName;
        }
    }
}
