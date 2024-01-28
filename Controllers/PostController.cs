using System.Security.Claims;
using AutoMapper;
using Forum_Management_System.Exceptions;
using Forum_Management_System.Models;
using Forum_Management_System.Models.View;
using Forum_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Forum_Management_System.Controllers
{
    public class PostController : Controller
    {
        public readonly IPostsService _postsService;
        public readonly IMapper _mapper;
        public PostController(IPostsService postsService, IMapper mapper)
        {
            _postsService = postsService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Post/Preview/{id}")]
        public async Task<IActionResult> Preview(int id)
        {
            var authIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int? authId = authIdClaim != null ? int.Parse(authIdClaim) : (int?)null;

            var post = await _postsService.GetByID(id);
            var postView = _mapper.Map<PostViewModel>(post, opts =>
            {
                opts.Items["authId"] = authId;
            });
            
            return View(postView);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
                var post = this._postsService.GetByID(id).Result;
                
                return this.View(post);
            }
            catch (EntityNotFoundException ex)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = ex.Message;

                return this.View("Error");

            }
        }

        [HttpGet]
        [Route("Post/Reload/{id}")]
        public async Task<IActionResult> ReloadPost(int id)
        {
            var post = await _postsService.GetByID(id);

            var authIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int? authId = authIdClaim != null ? int.Parse(authIdClaim) : (int?)null;

            var postView = _mapper.Map<PostViewModel>(post, opts =>
            {
                opts.Items["authId"] = authId;
            });

            return PartialView("_PostPartial", postView);
        }

        [HttpPost]
        [Route("Post/Like/{postId}")]
        public async Task<IActionResult> LikePost(int postId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _postsService.Like(int.Parse(userId), postId);
            return Ok();
        }

        [HttpPost]
        [Route("Post/RemoveLike/{postId}")]
        public async Task<IActionResult> RemoveLikePost(int postId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _postsService.RemoveLike(int.Parse(userId), postId);
            return Ok();
        }

        [HttpPost]
        [Route("Post/Dislike/{postId}")]
        public async Task<IActionResult> DislikePost(int postId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _postsService.Dislike(int.Parse(userId), postId);
            return Ok();
        }

        [HttpPost]
        [Route("Post/RemoveDislike/{postId}")]
        public async Task<IActionResult> RemoveDislikePost(int postId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _postsService.RemoveDislike(int.Parse(userId), postId);
            return Ok();
        }
    }
}
