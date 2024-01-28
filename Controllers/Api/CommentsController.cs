using Forum_Management_System.Exceptions;
using Forum_Management_System.Models.DTO;
using Forum_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Forum_Management_System.Controllers.Api
{
    //[ApiController]
    //[Route("api/comments")]
    //public class CommentsController : ControllerBase
    //{
    //    private readonly ICommentsService _commentsService;

    //    public CommentsController(ICommentsService commentsService)
    //    {
    //        _commentsService = commentsService;
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> Create([FromBody] CreateCommentDTO createCommentDTO)
    //    {
    //        try
    //        {
    //            UserFromTokenDTO user = ExtractUserFromToken();
    //            var createdComment = await _commentsService.Create(createCommentDTO, user);
    //            return Created(string.Empty, createdComment);
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

    //    [HttpPost("{id}")]
    //    public async Task<IActionResult> CreateReply(int id, [FromBody] ReplyDTO replyDTO)
    //    {
    //        try
    //        {
    //            UserFromTokenDTO user = ExtractUserFromToken();
    //            var createdReply = await _commentsService.Create(replyDTO, id, user);
    //            return Created(string.Empty, createdReply);
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
    //        catch (EntityNotFoundException ex)
    //        {
    //            return NotFound(ex.Message);
    //        }
    //    }

    //    [HttpPut("{id}")]
    //    public async Task<IActionResult> Update(int id, [FromBody] UpdateCommentDTO commentDTO)
    //    {
    //        try
    //        {
    //            UserFromTokenDTO user = ExtractUserFromToken();
    //            var updatedComment = await _commentsService.Update(id, commentDTO, user);
    //            return Ok(updatedComment);
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
    //        catch (EntityNotFoundException ex)
    //        {
    //            return NotFound(ex.Message);
    //        }
    //    }

    //    [HttpDelete("{id}")]
    //    public async Task<IActionResult> Delete(int id)
    //    {
    //        try
    //        {
    //            UserFromTokenDTO user = ExtractUserFromToken();
    //            await _commentsService.Delete(id, user);
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
    //        catch (EntityNotFoundException ex)
    //        {
    //            return NotFound(ex.Message);
    //        }
    //    }

    //    [HttpGet("{id}")]
    //    public async Task<IActionResult> Get(int id)
    //    {

    //        try
    //        {
    //            var comment = await _commentsService.Get(id);
    //            return Ok(comment);
    //        }
    //        catch (EntityNotFoundException ex)
    //        {
    //            return NotFound(ex.Message);
    //        }
    //    }

    //    [HttpGet("post/{postId}")]
    //    public async Task<IActionResult> GetByPostId(int postId)
    //    {
    //        var comments = await _commentsService.GetByPostId(postId);
    //        return Ok(comments);
    //    }

    //    [HttpPost("{id}/likes")]
    //    public async Task<IActionResult> ReceiveLike([FromRoute] int id)
    //    {
    //        try
    //        {
    //            UserFromTokenDTO user = ExtractUserFromToken();
    //            await _commentsService.ReceiveLike(id, user);

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
    //    }

    //    [HttpPost("{id}/dislikes")]
    //    public async Task<IActionResult> Dislike([FromRoute] int id)
    //    {
    //        try
    //        {
    //            UserFromTokenDTO user = ExtractUserFromToken();
    //            await _commentsService.Dislike(id, user);

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
    //    }

    //    [HttpPut("Block/{id}")]
    //    public async Task<IActionResult> BlockComment(int id)
    //    {
    //        try
    //        {
    //            UserFromTokenDTO admin = ExtractUserFromToken();

    //            GetCommentDTO updateComment = await _commentsService.Block(admin, id);

    //            return Ok(updateComment);
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
