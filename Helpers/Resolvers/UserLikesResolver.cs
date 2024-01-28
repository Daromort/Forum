using AutoMapper;
using Forum_Management_System.Models.View;
using Forum_Management_System.Models;
using Forum_Management_System.Services.Interfaces;

namespace Forum_Management_System.Helpers.Resolvers
{
    public class UserLikesResolver : IValueResolver<Comment, CommentViewModel, bool>, IValueResolver<Post, PostViewModel, bool>, IValueResolver<Post, PostViewModelMini, bool>
    {
        private readonly ICommentsService _commentsService;
        private readonly IPostsService _postsService;

        public UserLikesResolver(ICommentsService commentsService, IPostsService postsService)
        {
            _commentsService = commentsService;
            _postsService = postsService;
        }

        public bool Resolve(Comment source, CommentViewModel destination, bool destMember, ResolutionContext context)
        {
            var authId = (int?)context.Items["authId"];
            return authId.HasValue && _commentsService.HasUserLikedComment(authId.Value, source.ID).Result;
        }

        public bool Resolve(Post source, PostViewModel destination, bool destMember, ResolutionContext context)
        {
            var authId = (int?)context.Items["authId"];
            return authId.HasValue && _postsService.HasUserLikedPost(authId.Value, source.ID).Result;
        }

        public bool Resolve(Post source, PostViewModelMini destination, bool destMember, ResolutionContext context)
        {
            var authId = (int?)context.Items["authId"];
            return authId.HasValue && _postsService.HasUserLikedPost(authId.Value, source.ID).Result;
        }
    }
}
