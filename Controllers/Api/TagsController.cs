using Forum_Management_System.Exceptions;
using Forum_Management_System.Models;
using Forum_Management_System.Models.DTO;
using Forum_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Forum_Management_System.Controllers.Api
{
    [ApiController]
    [Route("api/tags")]
    public class TagsController : ControllerBase
    {
        private readonly ITagsService _tagsService;

        public TagsController(ITagsService tagsService)
        {
            _tagsService = tagsService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TagDTO>> Get([FromRoute] int id)
        {
            try
            {
                var tag = await _tagsService.Get(id);
                return Ok(tag);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<ActionResult<TagDTO>> Get([FromQuery] TagQueryParameters parameters)
        {
            try
            {
                if (parameters.ID != null && parameters.ID != 0)
                {
                    var tag = await _tagsService.Get(parameters.ID);
                    return Ok(tag);
                }
                else if (parameters.Name != null)
                {
                    var tag = await _tagsService.Get(parameters.Name);
                    return Ok(tag);
                }

                return BadRequest();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(TagDTO tagDTO)
        {
            try
            {
                await _tagsService.Create(tagDTO);
                return Created(string.Empty, tagDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _tagsService.Delete(id);
                return NoContent();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] TagQueryParameters parameters)
        {
            try
            {
                if (parameters.ID != null && parameters.ID != 0)
                {
                    await _tagsService.Delete(parameters.ID);
                    return NoContent();
                }
                else if (parameters.Name != null)
                {
                    await _tagsService.Delete(parameters.Name);
                    return NoContent();
                }

                return BadRequest();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
