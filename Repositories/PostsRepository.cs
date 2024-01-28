using Forum_Management_System.Exceptions;
using Forum_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Forum_Management_System.Data;
using AutoMapper;
using Forum_Management_System.Models.DTO;
using Forum_Management_System.Models.Enums;
using Forum_Management_System.Repositories.Interfaces;
using AutoMapper.QueryableExtensions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;

namespace Forum_Management_System.Repositories
{
    public class PostsRepository : IPostsRepository
    {

        private readonly ForumDbContext _context;
        private readonly IMapper _mapper;

        public PostsRepository(ForumDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Save(Post post)
        {
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Post>> Get(PostQueryParameters parameters)
        {
            var query = _context.Posts
            .Include(post => post.Tags)
            .Include(post => post.Comments.Where(c => !c.IsBlocked))
            .ThenInclude(comment => comment.User)
            .AsQueryable();

            if (parameters.ID.HasValue)
            {
                var post = await query.Where(x => !x.IsBlocked).FirstOrDefaultAsync(p => p.ID == parameters.ID.Value);
                return post != null ? new List<Post> { post } : null;
            }

            if (!string.IsNullOrEmpty(parameters.Title))
            {
                query = query.Where(p => p.Title.Contains(parameters.Title) && !p.IsBlocked);
            }

            if (!string.IsNullOrEmpty(parameters.Username))
            {
                query = query.Where(p => p.User.Username.Contains(parameters.Username) && p.IsBlocked);
            }

            return await query
            .Where(p => !p.IsBlocked)
            .Skip((parameters.Page - 1) * parameters.Size)
            .Take(parameters.Size)
            .ToListAsync();
        }

        public async Task<ICollection<Post>> GetBlocked(PostQueryParameters parameters)
        {
            var query = _context.Posts
            .Include(post => post.Tags)
            .Include(post => post.Comments)
            .ThenInclude(comment => comment.User)
            .AsQueryable();

            if (parameters.ID.HasValue)
            {
                var post = await query.FirstOrDefaultAsync(p => p.ID == parameters.ID.Value);
                return post != null ? new List<Post> { post } : null;
            }

            if (!string.IsNullOrEmpty(parameters.Title))
            {
                query = query.Where(p => p.Title.Contains(parameters.Title));
            }

            if (!string.IsNullOrEmpty(parameters.Username))
            {
                query = query.Where(p => p.User.Username.Contains(parameters.Username));
            }

            return await query
            .Where(p => !p.IsBlocked)
            .Skip((parameters.Page - 1) * parameters.Size)
            .Take(parameters.Size)
            .ToListAsync();
        }

        public async Task<Post> GetByID(int id)
        {
            return await _context.Posts
            .Include(post => post.Tags)
            .Include(post => post.User)
            .Include(post => post.Likes)
            .Include(post => post.Dislikes)
            .Include(post => post.Comments.Where(c => !c.IsBlocked))
            .ThenInclude(comment => comment.User)
            .FirstOrDefaultAsync(post => post.ID == id);
        }

        public async Task<Post> Create(CreatePostDTO postDTO, User user)
        {

            var post = _mapper.Map<Post>(postDTO);
            post.CreatedAt = DateTime.UtcNow;
            post.UserID = user.ID;

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return post;
        }

        public async Task Delete(Post post)
        {
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task Like(int userId, int postId)
        {
            var post = await _context.Posts.Include(p => p.User).FirstOrDefaultAsync(p => p.ID == postId);
            var like = new PostLike { UserID = userId, PostID = post.ID };
            _context.PostsLikes.Add(like);
            post.User.Karma++;
            post.LikesCount++;
            await _context.SaveChangesAsync();
        }

        public async Task RemoveLike(int userId, int postId)
        {
            var like = await _context.PostsLikes.SingleOrDefaultAsync(pl => pl.UserID == userId && pl.PostID == postId);
            if (like != null)
            {
                _context.PostsLikes.Remove(like);
                var post = await _context.Posts.Include(p => p.User).FirstOrDefaultAsync(p => p.ID == postId);
                post.User.Karma--;
                post.LikesCount--;
                await _context.SaveChangesAsync();
            }
        }

        public async Task Dislike(int userId, int postId)
        {
            var post = await _context.Posts.Include(p => p.User).FirstOrDefaultAsync(p => p.ID == postId);
            var dislike = new PostDislike { UserID = userId, PostID = postId };
            _context.PostsDislikes.Add(dislike);
            post.User.Karma--;
            post.LikesCount--;
            await _context.SaveChangesAsync();
        }

        public async Task RemoveDislike(int userId, int postId)
        {
            var dislike = await _context.PostsDislikes.SingleOrDefaultAsync(pd => pd.UserID == userId && pd.PostID == postId);
            if (dislike != null)
            {
                _context.PostsDislikes.Remove(dislike);
                var post = await _context.Posts.Include(p => p.User).FirstOrDefaultAsync(p => p.ID == postId);
                post.User.Karma++;
                post.LikesCount++;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Post> Block(int id)
        {
            Post postToBlock = this.GetByID(id).Result;

            if (postToBlock != null)
            {
                postToBlock.IsBlocked = true;
                await _context.SaveChangesAsync();
            }

            return postToBlock;
        }

        public async Task<Post> Unblock(int id)
        {

            Post postToUnBlock = this.GetByID(id).Result;

            if (postToUnBlock != null)
            {
                postToUnBlock.IsBlocked = false;
                await _context.SaveChangesAsync();
            }

            return postToUnBlock;
        }

        public async Task<ICollection<Post>> Search(PostQueryParameters parameters, FilterParameters? filterParameters = null)
        {
            var query = await SearchQuery(parameters, filterParameters);
            return await query.ToListAsync();
        }
        public async Task<IQueryable<Post>> SearchQuery(PostQueryParameters parameters, FilterParameters? filterParameters = null)
        {
            IQueryable<Post> query = _context.Posts;

            //Exclude blocked posts
            query = query.Where(u => !u.IsBlocked);

            if (filterParameters != null)
            {
                if (filterParameters.UserID != null)
                {
                    query = query.Where(u => u.UserID == filterParameters.UserID);
                }
                if (filterParameters.GroupID != null)
                {
                    query = query.Where(u => u.GroupID == filterParameters.GroupID);
                }
            }

            // Search based on Tags or based on Title
            if (parameters.Tags != null && parameters.Tags.Any())
            {
                var tagIds = parameters.Tags.Select(t => t.ID).ToList();
                query = query.Where(p => p.PostTag.All(pt => tagIds.Contains(pt.TagID)));
            }
            else if (parameters.Search != null)
            {
                query = query.Where(p => p.Title.Contains(parameters.Search));
            }

            if (parameters.Sort == SortBy.Popular)
            {
                query = query.OrderByDescending(p => p.LikesCount);   
            }
            else if (parameters.Sort == SortBy.Newest)
            {
                query = query.OrderByDescending(p => p.CreatedAt);
            }

            // Pagination
            query = query.Skip((parameters.Page - 1) * parameters.Size).Take(parameters.Size);

            query = query.Include(p => p.User)
                .Include(p => p.Group);

            return query;
        }
        public async Task<bool> CheckNext(PostQueryParameters parameters, FilterParameters? filterParameters = null)
        {
            parameters.Page += 1;
            var query = await SearchQuery(parameters, filterParameters);
            var post = await query.FirstOrDefaultAsync();
            return post != null;
        }
        public Task<bool> HasUserLikedPost(int userId, int postId)
        {
            return _context.PostsLikes.AnyAsync(x => x.UserID == userId && x.PostID == postId);
        }
        public async Task<bool> HasUserDislikedPost(int userId, int postId)
        {
            return await _context.PostsDislikes.AnyAsync(pd => pd.UserID == userId && pd.PostID == postId);
        }
    }
}
