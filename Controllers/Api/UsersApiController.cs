using Forum_Management_System.Exceptions;
using Forum_Management_System.Models;
using Forum_Management_System.Models.DTO;
using Forum_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Forum_Management_System.Controllers.Api
{
    [ApiController]
    [Route("api/users")]
    public class UsersApiController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersApiController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet("")]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] UserQueryParameters parameters)
        {
            ICollection<GetUserDTO> users = await _usersService.GetAll(parameters);
            if (!users.Any())
            {
                return Ok("No results found.");
            }

            return Ok(users);
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO user)
        {
            try
            {
                GetUserDTO createdUser = await _usersService.Create(user);
                return Ok(createdUser);
            }
            catch (Exception ex) when (ex is DuplicateEntityException || ex is DuplicatedEmailException)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            try
            {
                //GetUserDTO updatedUser = await _usersService.Update(user);

                return Ok();
            }
            catch (EntityNotFoundException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("Block/{id}")]
        public async Task<IActionResult> BlockUser(int id, [FromHeader] string credentials)
        {
            try
            {
                UserFromTokenDTO admin = ExtractUserFromToken();
                User user = await _usersService.GetUserByID(id);
                GetUserDTO updatedUser = await _usersService.Block(user, admin);

                return Ok(updatedUser);
            }
            catch (EntityNotFoundException ex)
            {
                return Conflict(ex.Message);
            }
            catch (UnauthenticatedOperationException e)
            {
                return StatusCode(StatusCodes.Status403Forbidden, e.Message);
            }
            catch (UnauthorizedOperationException e)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
        }

        [HttpPut("Unblock/{id}")]
        public async Task<IActionResult> UnblockUser(int id, [FromHeader] string credentials)
        {
            try
            {
                UserFromTokenDTO admin = ExtractUserFromToken();
                User user = await _usersService.GetUserByIDBlocked(id);
                GetUserDTO updatedUser = await _usersService.Unblock(user, admin);

                return Ok(updatedUser);
            }
            catch (EntityNotFoundException ex)
            {
                return Conflict(ex.Message);
            }
            catch (UnauthenticatedOperationException e)
            {
                return StatusCode(StatusCodes.Status403Forbidden, e.Message);
            }
            catch (UnauthorizedOperationException e)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
        }

        private UserFromTokenDTO ExtractUserFromToken()
        {
            UserFromTokenDTO user = new UserFromTokenDTO();
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                user.Username = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                user.Email = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                user.Role = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            }

            return user;
        }
    }
}
