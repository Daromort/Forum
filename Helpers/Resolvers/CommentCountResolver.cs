using AutoMapper;
using Forum_Management_System.Models.View;
using Forum_Management_System.Models;
using Forum_Management_System.Services.Interfaces;

namespace Forum_Management_System.Helpers.Resolvers
{
    public class CommentCountResolver : IValueResolver<User, UserViewModel, int>
    {
        private readonly IUsersService _userService;

        public CommentCountResolver(IUsersService userService)
        {
            _userService = userService;
        }

        public int Resolve(User source, UserViewModel destination, int destMember, ResolutionContext context)
        {
            var commentCount = Task.Run(async () => await _userService.GetCommentCount(source.ID)).GetAwaiter().GetResult();
            return commentCount;
        }
    }
}
