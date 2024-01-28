using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Xml.Linq;
using AutoMapper;
using Forum_Management_System.Models;
using Forum_Management_System.Models.DTO;
using Forum_Management_System.Models.View;
using Forum_Management_System.Services;
using Forum_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum_Management_System.Controllers
{
    public class UserController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUsersService _usersService;
        private readonly IPostsService _postsService;
        private readonly IGroupService _groupsService;
        private readonly ICommentsService _commentsService;
        private readonly IMapper _mapper;

        public UserController(IAuthenticationService authenticationService, IUsersService usersService, IMapper mapper, IPostsService postsService, IGroupService groupsService, ICommentsService commentsService)
        {
            _authenticationService = authenticationService;
            _usersService = usersService;
            _mapper = mapper;
            _postsService = postsService;
            _groupsService = groupsService;
            _commentsService = commentsService;
        }
        public async Task<IActionResult> Profile()
        {
            var currentUser = ExtractUserFromToken();
            var userInfo = await this._usersService.GetUserByEmail(currentUser.Email);
            PopulateViewData(userInfo);
            UserProfileViewModel viewModel = new UserProfileViewModel();

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Profile(UserProfileViewModel viewModel)
        {
            string userEmail = ExtractUserFromToken().Email;
            User user = this._mapper.Map<User>(viewModel);
            await this._usersService.Update(userEmail, user);

            return RedirectToAction("Profile", "User");
        }


        [HttpPost]
        public async Task<IActionResult> AddImage()
        {
            var email = ExtractUserFromToken().Email;
            var file = Request.Form.Files[0];
            var imageBytes = new byte[2 * 1024 * 1024];
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                imageBytes = memoryStream.ToArray();
            }
            await this._usersService.AddImage(email, imageBytes);
            return RedirectToAction("Profile", "User");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateTwitter(string twitterLink)
        {
            var email = ExtractUserFromToken().Email;
            await this._usersService.AddTwitterAccount(email, twitterLink);
            return RedirectToAction("Profile", "User");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateFacebook(string facebookLink)
        {
            var email = ExtractUserFromToken().Email;
            await this._usersService.AddFacebookAccount(email, facebookLink);

            return RedirectToAction("Profile", "User");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateInstagram(string instagramLink)
        {
            var email = ExtractUserFromToken().Email;
            await this._usersService.AddInstagramAccount(email, instagramLink);
            return RedirectToAction("Profile", "User");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var claimID = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (claimID != null)
            {
                ViewBag.User = await _usersService.GetUserByID(int.Parse(claimID));
            }
            else
            {
                throw new UnauthorizedAccessException("Something went terribly wrong with the claim, please contact our support team.");
            }
            return PartialView("_UserViewBag");
        }
        [HttpGet]
        public async Task<IActionResult> Display(int id)
        {
            var user = await _usersService.GetUserByID(id);

            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> Preview(QueryParameters parameters)
        {
            return View("Preview", parameters);
        }

        [Route("User/GetUserBanner/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetUserBanner(int id)
        {
            try
            {
                var user = await _usersService.GetUserByID(id);
                var userView = _mapper.Map<UserViewModel>(user);
                return PartialView("_UserBanner", userView);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("User/GetUserPostsPartial/{userId}")]
        [HttpGet]
        public async Task<IActionResult> GetUserPostsPartial(int userId, [FromQuery] PostQueryParameters queryParameters)
        {
            var filterParameters = new FilterParameters { UserID = userId };
            var posts = await _postsService.Search(queryParameters, filterParameters);

            var authIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int? authId = authIdClaim != null ? int.Parse(authIdClaim) : (int?)null;
            
            var postsView = _mapper.Map<ICollection<PostViewModelMini>>(posts, opts =>
            {
                opts.Items["authId"] = authId;
            });
            return PartialView("_PostsPartial", postsView);
        }

        [Route("User/GetUserCommentsPartial/{userId}")]
        [HttpGet]
        public async Task<IActionResult> GetUserCommentsPartial(int userId, [FromQuery] QueryParameters queryParameters)
        {
            var filterParameters = new FilterParameters { UserID = userId };
            var comments = await _commentsService.Search(queryParameters, filterParameters);

            var authIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int? authId = authIdClaim != null ? int.Parse(authIdClaim) : (int?)null;

            var commentsView = _mapper.Map<ICollection<CommentViewModel>>(comments, opts =>
            {
                opts.Items["authId"] = authId;
            });

            return PartialView("Views/Shared/_CommentsPartial.cshtml", commentsView);
        }

        [Route("User/GetUserGroupsPartial/{userId}")]
        [HttpGet]
        public async Task<IActionResult> GetUserGroupsPartial(int userId, [FromQuery] QueryParameters queryParameters)
        {
            var filterParameters = new FilterParameters { UserID = userId };
            var groups = await _groupsService.Search(queryParameters, filterParameters);
            return PartialView("_GroupsPartial", groups);
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
        private void PopulateViewData(User userInfo)
        {
            ViewData.Add("firstName", userInfo.FirstName);
            ViewData.Add("lastName", userInfo.LastName);
            ViewData.Add("email", userInfo.Email);
            ViewData.Add("phone", userInfo.PhoneNumber);
            ViewData.Add("photo", userInfo.Photo);
        }

        [Route("User/CheckNextPosts/{userId}")]
        [HttpGet]
        public async Task<IActionResult> CheckNextPosts(int userId, [FromQuery] PostQueryParameters parameters)
        {
            var filterParameters = new FilterParameters { UserID = userId };
            var hasMore = await _postsService.CheckNext(parameters, filterParameters);
            return Ok(hasMore);
        }
        [Route("User/CheckNextGroups/{userId}")]
        [HttpGet]
        public async Task<IActionResult> CheckNextGroups(int userId, [FromQuery] QueryParameters parameters)
        {
            var filterParameters = new FilterParameters { UserID = userId };
            var hasMore = await _groupsService.CheckNext(parameters, filterParameters);
            return Ok(hasMore);
        }
        [Route("User/CheckNextComments/{userId}")]
        [HttpGet]
        public async Task<IActionResult> CheckNextComments(int userId, [FromQuery] QueryParameters parameters)
        {
            var filterParameters = new FilterParameters { UserID = userId };
            var hasMore = await _commentsService.CheckNext(parameters, filterParameters);
            return Ok(hasMore);
        }
    }
}