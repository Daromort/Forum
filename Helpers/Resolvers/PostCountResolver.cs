using AutoMapper;
using Forum_Management_System.Models.View;
using Forum_Management_System.Models;
using Forum_Management_System.Services.Interfaces;

namespace Forum_Management_System.Helpers.Resolvers
{
    public class PostsCountResolver : IValueResolver<User, UserViewModel, int>
    {
        private readonly IUsersService _userService;

        public PostsCountResolver(IUsersService userService)
        {
            _userService = userService;
        }

            public int Resolve(User source, UserViewModel destination, int destMember, ResolutionContext context)
        {
            var postsCount = Task.Run(async () => await _userService.GetPostsCount(source.ID)).GetAwaiter().GetResult();
            return postsCount;
        }
    }
}