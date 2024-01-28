using System.Formats.Asn1;
using System.Globalization;
using AutoMapper;
using Forum_Management_System.Data;
using Forum_Management_System.Models;
using Forum_Management_System.Models.DTO;
using Forum_Management_System.Models.Enums;
using Forum_Management_System.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Forum_Management_System.Repositories
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly ForumDbContext _context;
        private readonly IMapper _mapper;

        public CommentsRepository(ForumDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //ToDo
        //Ask Yasen if there is any chance for the _context.Comments.Add to be executed before _context.SaveChangesAsync()
        public async Task<Comment> Create(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment> Update(Comment comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task Delete(Comment comment)
        {
            await DeleteReplies(comment);
            await _context.SaveChangesAsync();
        }

        //Add DeleteCommentWithReplies method that will be called from delete, that will recursively delete all replies, starting from the lowest layer.
        public async Task DeleteReplies(Comment comment)
        {
            foreach (var reply in comment.Replies)
            {
                await DeleteReplies(reply);
            }
            _context.Comments.Remove(comment);
        }

        //ToDo - Add different gets to optimize deletion and updates? Everywhere?
        public async Task<Comment> Get(int id)
        {
            var comment = await _context.Comments
            .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.ID == id && !c.IsBlocked);
            if (comment != null)
            {
                await LoadRepliesRecursively(comment);
            }
            return comment;
        }

        public async Task<ICollection<Comment>> GetByPostId(int postId)
        {
            var comments = await _context.Comments
                .Include(c => c.User)
                .Where(c => c.PostID == postId && c.ParentID == null && !c.IsBlocked)
                .ToListAsync();
            if (comments.Any())
            {
                await LoadRepliesRecursively(comments);
            }
            return comments;
        }

        public async Task LoadRepliesRecursively(ICollection<Comment> comments)
        {
            foreach (var comment in comments)
            {
                comment.Replies = await _context.Comments
                    .Include(c => c.User)
                    .Where(c => c.ParentID == comment.ID && !c.IsBlocked)
                    .ToListAsync();

                await LoadRepliesRecursively(comment.Replies);
            }
        }
        public async Task LoadRepliesRecursively(Comment comment)
        {
            comment.Replies = await _context.Comments
                .Include(c => c.User)
                .Where(c => c.ParentID == comment.ID && !c.IsBlocked)
                .ToListAsync();

            foreach (var reply in comment.Replies)
            {
                await LoadRepliesRecursively(reply);
            }
        }
        public async Task Like(int userId, int commentId)
        {
            var comment = await _context.Comments.Include(c => c.User).FirstOrDefaultAsync(c => c.ID == commentId);
            var like = new CommentLike { UserID = userId, CommentID = comment.ID };
            _context.CommentsLikes.Add(like);
            comment.User.Karma++;
            comment.LikesCount++;
            await _context.SaveChangesAsync();
        }

        public async Task RemoveLike(int userId, int commentId)
        {
            var like = await _context.CommentsLikes.SingleOrDefaultAsync(cl => cl.UserID == userId && cl.CommentID == commentId);
            if (like != null)
            {
                _context.CommentsLikes.Remove(like);
                var comment = await _context.Comments.Include(c => c.User).FirstOrDefaultAsync(c => c.ID == commentId);
                comment.User.Karma--;
                comment.LikesCount--;
                await _context.SaveChangesAsync();
            }
        }

        public async Task Dislike(int userId, int commentId)
        {
            var comment = await _context.Comments.Include(c => c.User).FirstOrDefaultAsync(c => c.ID == commentId);
            var dislike = new CommentDislike { UserID = userId, CommentID = comment.ID };
            _context.CommentsDislikes.Add(dislike);
            comment.User.Karma--;
            comment.LikesCount--;
            await _context.SaveChangesAsync();
        }

        public async Task RemoveDislike(int userId, int commentId)
        {
            var dislike = await _context.CommentsDislikes.SingleOrDefaultAsync(cd => cd.UserID == userId && cd.CommentID == commentId);
            if (dislike != null)
            {
                _context.CommentsDislikes.Remove(dislike);
                var comment = await _context.Comments.Include(c => c.User).FirstOrDefaultAsync(c => c.ID == commentId);
                comment.User.Karma++;
                comment.LikesCount++;
                await _context.SaveChangesAsync();
            }
        }


        public async Task<Comment> Block(int id)
        {
            var commentToBlock = this.Get(id).Result;

            if (commentToBlock != null)
            {
                commentToBlock.IsBlocked = true;
                await _context.SaveChangesAsync();
            }

            return commentToBlock;
        }

        public async Task<Comment> UnBlock(int id)
        {

            var commentToUnBlock = this.Get(id).Result;

            if (commentToUnBlock != null)
            {
                commentToUnBlock.IsBlocked = false;
                await _context.SaveChangesAsync();
            }

            return commentToUnBlock;
        }

        private async Task<IQueryable<Comment>> SearchQuery(QueryParameters parameters, FilterParameters? filterParameters = null)
        {
            IQueryable<Comment> query = _context.Comments;

            //Exclude blocked posts
            query = query.Where(c => !c.IsBlocked);

            if (filterParameters != null)
            {
                if (filterParameters.UserID != null)
                {
                    query = query.Where(c => c.UserID == filterParameters.UserID);
                }
            }

            if (parameters.Sort == SortBy.Popular)
            {
                query = query.OrderByDescending(c => c.LikesCount);
            }
            else if (parameters.Sort == SortBy.Newest)
            {
                query = query.OrderByDescending(c => c.CreatedAt);
            }

            // Pagination
            query = query.Skip((parameters.Page - 1) * parameters.Size).Take(parameters.Size);

            query = query.Include(c => c.User)
                .Include(c => c.Post);


            return query;
        }
        public async Task<ICollection<Comment>> Search(QueryParameters parameters, FilterParameters? filterParameters = null)
        {
            var query = await SearchQuery(parameters, filterParameters);
            return await query.ToListAsync();
        }

        public async Task<bool> CheckNext(QueryParameters parameters, FilterParameters? filterParameters = null)
        {
            parameters.Page += 1;
            var query = await SearchQuery(parameters, filterParameters);
            var post = await query.FirstOrDefaultAsync();
            return post != null;
        }
        public async Task<bool> HasUserLikedComment(int userId, int commentId)
        {
            return await _context.CommentsLikes.AnyAsync(x => x.UserID == userId && x.CommentID == commentId);
        }
        public async Task<bool> HasUserDislikedComment(int userId, int commentId)
        {
            return await _context.CommentsDislikes.AnyAsync(x => x.UserID == userId && x.CommentID == commentId);
        }
        public async Task<ICollection<Comment>> GetPostComments(int postId)
        {
            return await _context.Comments
                .Include(c => c.User)
                .Where(c => c.PostID == postId && c.ParentID == null)
                .ToListAsync();
        }
        public async Task<ICollection<Comment>> GetReplies(int commentId)
        {
            return await _context.Comments
                .Include(c => c.User)
                .Where(c => c.ParentID == commentId)
                .ToListAsync();
        }
        public async Task<bool> HasReplies(int commentId)
        {
            return await _context.Comments
                .AnyAsync(c => c.ParentID == commentId);
        }
    }
}
