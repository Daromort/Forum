using Forum_Management_System.Exceptions;
using Forum_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Forum_Management_System.Controllers.Api
{

    [ApiController]
    [Route("api/groups")]
    public class GroupConroller : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupConroller(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            try
            {
                var group = await _groupService.GetById(id);


                if (group == null)
                {
                    return NotFound();
                }

                return Ok(group);
            }
            catch (EntityNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
        }
    }
}
