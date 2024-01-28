using System.Security.Claims;
using AutoMapper;
using Forum_Management_System.Models;
using Forum_Management_System.Models.DTO;
using Forum_Management_System.Models.View;
using Forum_Management_System.Services;
using Forum_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Forum_Management_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostsService _postsService;
        private readonly IUsersService _usersService;
        private readonly IGroupService _groupsService;
        private readonly IMapper _mapper;

        public HomeController(IPostsService postsService, IUsersService usersService, IGroupService groupService, IMapper mapper)
        {
            _postsService = postsService;
            _usersService = usersService;
            _groupsService = groupService;
            _mapper = mapper;
        }

        public IActionResult Index(QueryParameters parameters)
        {
            return View(parameters);
        }

        public async Task<IActionResult> GetPostsPartial(PostQueryParameters parameters)
        {
            var posts = await _postsService.Search(parameters);
            var authIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int? authId = authIdClaim != null ? int.Parse(authIdClaim) : (int?)null;

            var postsView = _mapper.Map<ICollection<PostViewModelMini>>(posts, opts =>
            {
                opts.Items["authId"] = authId;
            });

            return PartialView("_PostsPartial", postsView);
        }

        public async Task<IActionResult> GetUsersPartial(UserQueryParameters parameters)
        {
            var users = await _usersService.Search(parameters);
            return PartialView("_UsersPartial", users);
        }

        public async Task<IActionResult> GetGroupsPartial(QueryParameters parameters)
        {
            var groups = await _groupsService.Search(parameters);
            return PartialView("_GroupsPartial", groups);
        }

        [HttpGet]
        public async Task<IActionResult> CheckNextPosts(PostQueryParameters parameters)
        {
            bool hasMore;
            hasMore = await _postsService.CheckNext(parameters);
            return Ok(hasMore);
        }
        [HttpGet]
        public async Task<IActionResult> CheckNextUsers(UserQueryParameters parameters)
        {
            bool hasMore;
            hasMore = await _usersService.CheckNext(parameters);
            return Ok(hasMore);
        }
        [HttpGet]
        public async Task<IActionResult> CheckNextGroups(QueryParameters parameters)
        {
            bool hasMore;
            hasMore = await _groupsService.CheckNext(parameters);
            return Ok(hasMore);
        }
    }
}

