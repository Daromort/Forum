using AutoMapper;
using Forum_Management_System.Exceptions;
using Forum_Management_System.Helpers;
using Forum_Management_System.Models;
using Forum_Management_System.Models.DTO;
using Forum_Management_System.Models.Enums;
using Forum_Management_System.Repositories.Interfaces;
using Forum_Management_System.Services.Interfaces;
using Forum_Management_System.Models.View;

namespace Forum_Management_System.Services
{
    public class PostsService : IPostsService
    {
        private const string ModifyPostErrorMessage = "Only owner or admin can modify a post.";
        private const string BlockErrorMessage = "Only admin can block or unblock posts!";

        public const byte DoublePointsScoreRate = 2;
        public const byte SinglePointScoreRate = 1;

        private readonly IPostsRepository _postsRepository;
        private readonly ITagsRepository _tagsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public PostsService(IPostsRepository postsRepository, IMapper mapper, ITagsRepository tagsRepository, IUsersRepository usersRepository)
        {
            _postsRepository = postsRepository;
            _mapper = mapper;
            _tagsRepository = tagsRepository;
            _usersRepository = usersRepository;
        }

        public async Task<ICollection<GetPostDTO>> Get(PostQueryParameters parameters)
        {
            var posts = await _postsRepository.Get(parameters);

            if (posts == null || !posts.Any())
            {
                throw new EntityNotFoundException("No posts found.");
            }

            return _mapper.Map<List<GetPostDTO>>(posts);
        }

        public async Task<ICollection<GetPostDTO>> GetBlocked(PostQueryParameters parameters)
        {
            var posts = await _postsRepository.GetBlocked(parameters);

            if (posts == null || !posts.Any())
            {
                throw new EntityNotFoundException("No posts found.");
            }

            return _mapper.Map<List<GetPostDTO>>(posts);
        }

        public async Task<GetPostDTO> Create(CreatePostDTO createPostDTO, UserFromTokenDTO userFromToken)
        {
            User userFromDatabase = await this._usersRepository.GetUserByEmail(userFromToken.Email);
            var post = await _postsRepository.Create(createPostDTO, userFromDatabase);

            if (createPostDTO.Tags != null && createPostDTO.Tags.Count > 0)
            {
                foreach (var tagDTO in createPostDTO.Tags)
                {
                    tagDTO.Name = tagDTO.Name.ToLower();
                    var tag = _mapper.Map<Tag>(tagDTO);
                    await _tagsRepository.Create(tag, post.ID);
                }
            }

            return _mapper.Map<GetPostDTO>(post);
        }

        public async Task<GetPostDTO> Update(int id, UpdatePostDTO updatePostDTO, UserFromTokenDTO user)
        {
            var post = await _postsRepository.GetByID(id);

            if (post.IsBlocked)
            {
                throw new BlockedPostException("This post is blocked!");
            }

            ThrowEntityNotFoundIfPostIsNull(post);

            if (user.Role != "Admin" && post.User.Email != user.Email)
            {
                throw new UnauthorizedAccessException(ModifyPostErrorMessage);
            }

            if (updatePostDTO.Title != null)
            {
                post.Title = updatePostDTO.Title;
            }

            if (updatePostDTO.Content != null)
            {
                post.Content = updatePostDTO.Content;
            }

            if (updatePostDTO.Tags != null)
            {
                HashSet<Tag> oldTags = new HashSet<Tag>(post.Tags);
                await _tagsRepository.RemoveFromPost(oldTags, post.ID);
                post.Tags.Clear();

                foreach (var tagDTO in updatePostDTO.Tags)
                {
                    tagDTO.Name = tagDTO.Name.ToLower();
                    await _tagsRepository.Create(_mapper.Map<Tag>(tagDTO), post.ID);
                }

                var tags = _mapper.Map<List<Tag>>(updatePostDTO.Tags);
                post.Tags = tags;
            }

            await _postsRepository.Save(post);
            return _mapper.Map<GetPostDTO>(post);
        }
        public async Task Delete(int id, UserFromTokenDTO user)
        {
            var post = await _postsRepository.GetByID(id);

            if (post == null)
            {
                throw new EntityNotFoundException("Post not found.");
            }

            if (user.Role != "Admin" && post.User.Email != user.Email)
            {
                throw new UnauthorizedAccessException(ModifyPostErrorMessage);
            }

            await _postsRepository.Delete(post);
        }

        //ToDo
        public async Task Like(int userId, int postId)
        {
            if (await _postsRepository.HasUserDislikedPost(userId, postId))
            {
                await _postsRepository.RemoveDislike(userId, postId);
            }
            await _postsRepository.Like(userId, postId);
        }

        public async Task RemoveLike(int userId, int postId)
        {
            await _postsRepository.RemoveLike(userId, postId);
        }

        public async Task Dislike(int userId, int postId)
        {
            if (await _postsRepository.HasUserLikedPost(userId, postId))
            {
                await _postsRepository.RemoveLike(userId, postId);
            }
            await _postsRepository.Dislike(userId, postId);
        }

        public async Task RemoveDislike(int userId, int postId)
        {
            await _postsRepository.RemoveDislike(userId, postId);
        }

        public async Task<Post> GetByID(int ID)
        {
            Post post = await this._postsRepository.GetByID(ID);
            ThrowEntityNotFoundIfPostIsNull(post);

            return post;
        }

        private async Task<PostLike> GetLike(Post toPost, User fromUser)
        {
            return await Task.Run(() => toPost.Likes.FirstOrDefault(l => l.UserID == fromUser.ID && l.PostID == toPost.ID));
        }

        private async Task<PostDislike> GetDislike(Post toPost, User fromUser)
        {
            return await Task.Run(() => toPost.Dislikes.FirstOrDefault(l => l.UserID == fromUser.ID && l.PostID == toPost.ID));
        }

        private void ThrowEntityNotFoundIfPostIsNull(Post post)
        {
            if (post == null)
            {
                throw new EntityNotFoundException("Post not found.");
            }
        }
        public async Task<GetPostDTO> Block(UserFromTokenDTO admin, PostQueryParameters parameters)
        {

            if (!(admin.Role == "Admin"))
            {
                throw new UnauthorizedAccessException(BlockErrorMessage);
            }
            var postToBlocks = await this._postsRepository.Get(parameters);
            var postToBlock = await this._postsRepository.Block(postToBlocks.FirstOrDefault().ID);

            return this._mapper.Map<GetPostDTO>(postToBlock);
        }

        public async Task<GetPostDTO> Unblock(UserFromTokenDTO admin, PostQueryParameters parameters)
        {

            if (!(admin.Role == "Admin"))
            {
                throw new UnauthorizedAccessException(BlockErrorMessage);
            }
            var postToUnBlocks = await this._postsRepository.GetBlocked(parameters);
            var postToUnBlock = await this._postsRepository.Unblock(postToUnBlocks.FirstOrDefault().ID);

            return this._mapper.Map<GetPostDTO>(postToUnBlock);
        }

        public async Task<ICollection<Post>> Search(PostQueryParameters parameters, FilterParameters? filterParameters = null)
        {
            var posts = await _postsRepository.Search(parameters, filterParameters);
            return posts;
        }

        public async Task<bool> CheckNext(PostQueryParameters parameters, FilterParameters? filterParameters = null)
        {
            return await _postsRepository.CheckNext(parameters, filterParameters);
        }
        public Task<bool> HasUserLikedPost(int userId, int postId)
        {
            return _postsRepository.HasUserLikedPost(userId, postId);
        }
        public async Task<bool> HasUserDislikedPost(int userId, int postId)
        {
            return await _postsRepository.HasUserDislikedPost(userId, postId);
        }
    }
}
