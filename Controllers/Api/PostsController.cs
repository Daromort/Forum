using System.Security.Claims;
using Forum_Management_System.Exceptions;
using Forum_Management_System.Models;
using Forum_Management_System.Models.DTO;
using Forum_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Forum_Management_System.Controllers.Api
{
    //[ApiController]
    //[Route("api/posts")]
    //public class PostsController : ControllerBase
    //{
    //    private readonly IPostsService _postsService;

    //    public PostsController(IPostsService postsService)
    //    {
    //        _postsService = postsService;
    //    }

    //    [HttpGet("")]
    //    public async Task<IActionResult> Get([FromQuery] PostQueryParameters parameters)
    //    {

    //        try
    //        {
    //            var posts = await _postsService.Get(parameters);
    //            if (posts == null || posts.Count == 0)
    //            {
    //                return NotFound();
    //            }

    //            return Ok(posts.Where(p => !p.IsBlocked));
    //        }
    //        catch (EntityNotFoundException ex)
    //        {
    //            return StatusCode(StatusCodes.Status404NotFound, ex.Message);
    //        }
    //    }

    //    [HttpGet("{id}")]
    //    public async Task<IActionResult> Get(int id)
    //    {
    //        var post = await _postsService.GetByID(id);

    //        if (post == null)
    //        {
    //            return NotFound();
    //        }

    //        return Ok(post);
    //    }

    //    [HttpPost("")]
    //    public async Task<IActionResult> Create([FromBody] CreatePostDTO postDTO)
    //    {
    //        try
    //        {
    //            UserFromTokenDTO user = ExtractUserFromToken();
    //            var createdPost = await _postsService.Create(postDTO, user);
    //            return Ok(createdPost);

    //            //postDTO.UserID = user.ID;
    //        }
    //        catch (UnauthenticatedOperationException e)
    //        {
    //            return StatusCode(StatusCodes.Status403Forbidden, e.Message);
    //        }
    //        catch (UnauthorizedOperationException e)
    //        {
    //            return StatusCode(StatusCodes.Status401Unauthorized, e.Message);
    //        }
    //        catch (BlockedUserException e)
    //        {
    //            return StatusCode(StatusCodes.Status403Forbidden, e.Message);
    //        }
    //    }

    //    [HttpPut("{id}")]
    //    public async Task<IActionResult> Update(int id, [FromBody] UpdatePostDTO updatePostDTO)
    //    {
    //        try
    //        {
    //            UserFromTokenDTO user = ExtractUserFromToken();
    //            var updatedPost = await _postsService.Update(id, updatePostDTO, user);
    //            return Ok(updatedPost);
    //        }
    //        catch (BlockedPostException e)
    //        {
    //            return StatusCode(StatusCodes.Status403Forbidden, e.Message);

    //        }
    //        catch (EntityNotFoundException)
    //        {
    //            return NotFound();
    //        }
    //        catch (UnauthenticatedOperationException e)
    //        {
    //            return StatusCode(StatusCodes.Status403Forbidden, e.Message);
    //        }
    //        catch (UnauthorizedOperationException e)
    //        {
    //            return StatusCode(StatusCodes.Status401Unauthorized, e.Message);
    //        }
    //        catch (BlockedUserException e)
    //        {
    //            return StatusCode(StatusCodes.Status403Forbidden, e.Message);
    //        }
    //    }

    //    [HttpDelete("{id}")]
    //    public async Task<IActionResult> Delete(int id)
    //    {
    //        try
    //        {
    //            UserFromTokenDTO user = ExtractUserFromToken();
    //            await _postsService.Delete(id, user);
    //            return NoContent();
    //        }
    //        catch (UnauthenticatedOperationException e)
    //        {
    //            return StatusCode(StatusCodes.Status403Forbidden, e.Message);
    //        }
    //        catch (UnauthorizedOperationException e)
    //        {
    //            return StatusCode(StatusCodes.Status401Unauthorized, e.Message);

    //        }
    //        catch (BlockedUserException e)
    //        {
    //            return StatusCode(StatusCodes.Status403Forbidden, e.Message);
    //        }
    //        catch (EntityNotFoundException)
    //        {
    //            return NotFound();
    //        }
    //    }

    //    [HttpPost("{id}/likes")]
    //    public async Task<IActionResult> ReceiveLike([FromRoute] int id)
    //    {
    //        try
    //        {
    //            UserFromTokenDTO user = ExtractUserFromToken();
    //            await _postsService.ReceiveLike(id, user);

    //            return Ok();
    //        }
    //        catch (UnauthenticatedOperationException e)
    //        {
    //            return Forbid(e.Message);
    //        }
    //        catch (UnauthorizedOperationException e)
    //        {
    //            return Unauthorized(e.Message);
    //        }
    //        catch (DuplicateLikeException e)
    //        {
    //            return BadRequest(e.Message);
    //        }
    //        catch (EntityNotFoundException e)
    //        {
    //            return NotFound(e.Message);
    //        }
    //        catch (BlockedPostException e)
    //        {
    //            return StatusCode(StatusCodes.Status403Forbidden, e.Message);

    //        }
    //        catch (BlockedUserException e)
    //        {
    //            return StatusCode(StatusCodes.Status403Forbidden, e.Message);
    //        }
    //    }

    //    [HttpPost("{id}/dislikes")]
    //    public async Task<IActionResult> Dislike([FromRoute] int id)
    //    {
    //        try
    //        {
    //            UserFromTokenDTO user = ExtractUserFromToken();
    //            await _postsService.Dislike(id, user);

    //            return Ok();
    //        }
    //        catch (UnauthenticatedOperationException e)
    //        {
    //            return Forbid(e.Message);
    //        }
    //        catch (UnauthorizedOperationException e)
    //        {
    //            return Unauthorized(e.Message);
    //        }
    //        catch (InvalidOperationException e)
    //        {
    //            return BadRequest(e.Message);
    //        }
    //        catch (EntityNotFoundException e)
    //        {
    //            return NotFound(e.Message);
    //        }
    //        catch (BlockedUserException e)
    //        {
    //            return StatusCode(StatusCodes.Status403Forbidden, e.Message);
    //        }
    //        catch (BlockedPostException e)
    //        {
    //            return StatusCode(StatusCodes.Status403Forbidden, e.Message);

    //        }
    //    }

    //    [HttpPut("Block")]
    //    public async Task<IActionResult> BlockPost([FromQuery] PostQueryParameters parameters)
    //    {
    //        try
    //        {
    //            UserFromTokenDTO admin = ExtractUserFromToken();
    //            //var post = await this._postsService.Get(parameters);
    //            GetPostDTO updatedPost = await _postsService.Block(admin, parameters);

    //            return Ok(updatedPost);
    //        }
    //        catch (EntityNotFoundException ex)
    //        {
    //            return Conflict(ex.Message);
    //        }
    //        catch (UnauthenticatedOperationException e)
    //        {
    //            return StatusCode(StatusCodes.Status403Forbidden, e.Message);
    //        }
    //        catch (UnauthorizedOperationException e)
    //        {
    //            return StatusCode(StatusCodes.Status401Unauthorized, e.Message);

    //        }
    //        catch (BlockedUserException e)
    //        {
    //            return StatusCode(StatusCodes.Status403Forbidden, e.Message);
    //        }
    //    }
    //    [HttpPut("Unblock")]
    //    public async Task<IActionResult> UnblockPost([FromQuery] PostQueryParameters parameters)
    //    {
    //        try
    //        {
    //            UserFromTokenDTO admin = ExtractUserFromToken();
    //            //var post = await this._postsService.Get(parameters);
    //            GetPostDTO updatedPost = await _postsService.Unblock(admin, parameters);

    //            return Ok(updatedPost);
    //        }
    //        catch (EntityNotFoundException ex)
    //        {
    //            return Conflict(ex.Message);
    //        }
    //        catch (UnauthenticatedOperationException e)
    //        {
    //            return StatusCode(StatusCodes.Status403Forbidden, e.Message);
    //        }
    //        catch (UnauthorizedOperationException e)
    //        {
    //            return StatusCode(StatusCodes.Status401Unauthorized, e.Message);

    //        }
    //        catch (BlockedUserException e)
    //        {
    //            return StatusCode(StatusCodes.Status403Forbidden, e.Message);
    //        }
    //    }

    //    private UserFromTokenDTO ExtractUserFromToken()
    //    {
    //        UserFromTokenDTO user = new UserFromTokenDTO();
    //        var identity = HttpContext.User.Identity as ClaimsIdentity;
    //        if (identity != null)
    //        {
    //            var userClaims = identity.Claims;
    //            user.Username = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
    //            user.Email = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
    //            user.Role = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
    //        }

    //        return user;
    //    }
    //}
}
