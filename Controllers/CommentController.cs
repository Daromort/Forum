using AutoMapper;
using Forum_Management_System.Models;
using Forum_Management_System.Models.View;
using Forum_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;
using System.Xml.Linq;

public class CommentController : Controller
{
    private readonly ICommentsService _commentsService;
    private readonly IMapper _mapper;
    public CommentController(ICommentsService commentsService, IMapper mapper)
    {
        _commentsService = commentsService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("Comment/Reload/{id}")]
    public async Task<IActionResult> ReloadComment(int id)
    {
        var comment = await _commentsService.Get(id);

        var authIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int? authId = authIdClaim != null ? int.Parse(authIdClaim) : (int?)null;

        var commentView = _mapper.Map<CommentViewModel>(comment, opts =>
        {
            opts.Items["authId"] = authId;
        });

        return PartialView("_CommentPartial", commentView);
    }

    [HttpPost]
    [Route("Comment/Like/{commentId}")]
    public async Task<IActionResult> LikeComment(int commentId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _commentsService.Like(int.Parse(userId), commentId);
        return Ok();
    }

    [HttpPost]
    [Route("Comment/RemoveLike/{commentId}")]
    public async Task<IActionResult> RemoveLikeComment(int commentId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _commentsService.RemoveLike(int.Parse(userId), commentId);
        return Ok();
    }

    [HttpPost]
    [Route("Comment/Dislike/{commentId}")]
    public async Task<IActionResult> DislikeComment(int commentId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _commentsService.Dislike(int.Parse(userId), commentId);
        return Ok();
    }

    [HttpPost]
    [Route("Comment/RemoveDislike/{commentId}")]
    public async Task<IActionResult> RemoveDislikeComment(int commentId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _commentsService.RemoveDislike(int.Parse(userId), commentId);
        return Ok();
    }
    [HttpGet]
    [Route("Comment/GetPostComments/{postId}")]
    public async Task<IActionResult> GetPostComments(int postId)
    {
        var authIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int? authId = authIdClaim != null ? int.Parse(authIdClaim) : (int?)null;

        var comments = await _commentsService.GetPostComments(postId);
        var commentsView = _mapper.Map<ICollection<CommentViewModel>>(comments, opts =>
        {
            opts.Items["authId"] = authId;
        });
        return PartialView("_PostCommentsPartial", commentsView);
    }
    [HttpGet]
    [Route("Comment/GetReplies/{commentId}")]
    public async Task<IActionResult> GetReplies(int commentId)
    {
        var authIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int? authId = authIdClaim != null ? int.Parse(authIdClaim) : (int?)null;

        var replies = await _commentsService.GetReplies(commentId);
        var repliesView = _mapper.Map<ICollection<CommentViewModel>>(replies, opts =>
        {
            opts.Items["authId"] = authId;
        });

        return PartialView("_PostCommentsPartial", repliesView);
    }

    [HttpGet]
    [Route("Comment/CheckReply/{commentId}")]
    public async Task<IActionResult> CheckReply(int commentId)
    {
        var hasReplies = await _commentsService.HasReplies(commentId);

        return Ok(hasReplies);
    }
}
