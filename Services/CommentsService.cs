using AutoMapper;
using Forum_Management_System.Exceptions;
using Forum_Management_System.Models;
using Forum_Management_System.Models.DTO;
using Forum_Management_System.Models.View;
using Forum_Management_System.Repositories;
using Forum_Management_System.Repositories.Interfaces;
using Forum_Management_System.Services.Interfaces;

namespace Forum_Management_System.Services
{
    public class CommentsService : ICommentsService
    {
        private const string ModifyCommentErrorMessage = "Only owner or admin can modify a comment.";
        private const string BlockErrorMessage = "Only admin can block or unblock comment!";

        public const byte DoublePointsScoreRate = 2;
        public const byte SinglePointScoreRate = 1;


        private readonly ICommentsRepository _commentsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public CommentsService(ICommentsRepository commentsRepository, IMapper mapper, IUsersRepository usersRepository)
        {
            _commentsRepository = commentsRepository;
            _mapper = mapper;
            _usersRepository = usersRepository;
        }

        public async Task<GetCommentDTO> Create(CreateCommentDTO createCommentDTO, UserFromTokenDTO user)
        {
            var comment = _mapper.Map<Comment>(createCommentDTO);
            comment.UserID = user.ID;
            var createdComment = await _commentsRepository.Create(comment);
            return _mapper.Map<GetCommentDTO>(createdComment);
        }

        //ToDo - review this method
        public async Task<GetCommentDTO> Create(ReplyDTO replyDTO, int parentID, UserFromTokenDTO user)
        {
            var comment = _mapper.Map<Comment>(replyDTO);
            var mapperComment = await _commentsRepository.Get(parentID);
            if (comment == null)
            {
                throw new EntityNotFoundException("Comment not found.");
            }
            if (mapperComment.IsBlocked)
            {
                throw new BlockedCommentException("Coment is blocked!");
            }
            comment.ParentID = mapperComment.ID;
            comment.PostID = mapperComment.PostID;
            comment.UserID = user.ID;
            var createdComment = await _commentsRepository.Create(comment);
            return _mapper.Map<GetCommentDTO>(createdComment);
        }

        public async Task<GetCommentDTO> Update(int id, UpdateCommentDTO commentDTO, UserFromTokenDTO user)
        {
            var comment = await _commentsRepository.Get(id);

            if (comment == null)
            {
                throw new EntityNotFoundException("Comment not found.");
            }


            if (user.Role != "Admin" && comment.User.Email != user.Email)
            {
                throw new UnauthorizedAccessException(ModifyCommentErrorMessage);
            }

            _mapper.Map(commentDTO, comment);
            var updatedComment = await _commentsRepository.Update(comment);
            return _mapper.Map<GetCommentDTO>(updatedComment);
        }

        public async Task Delete(int id, UserFromTokenDTO user)
        {
            var comment = await _commentsRepository.Get(id);

            if (comment == null)
            {
                throw new EntityNotFoundException("Comment not found.");
            }

            if (comment.IsBlocked)
            {
                throw new BlockedCommentException("Coment is blocked!");
            }

            if (user.Role == "Admin" || comment.User.Email == user.Email)
            {
                await _commentsRepository.Delete(comment);
            }
            else
            {
                throw new UnauthorizedAccessException(ModifyCommentErrorMessage);
            }
        }

        public async Task<Comment> Get(int id)
        {
            var comment = await _commentsRepository.Get(id);

            if (comment == null)
            {
                throw new EntityNotFoundException("Comment not found.");
            }
            if (comment.IsBlocked)
            {
                throw new BlockedCommentException("Coment is blocked!");
            }

            return comment;
        }

        //public async Task<GetCommentDTO> GetBlocked(int id)
        //{
        //    var comment = await _commentsRepository.Get(id);

        //    if (comment == null)
        //    {
        //        throw new EntityNotFoundException("Comment not found.");
        //    }


        //    return _mapper.Map<GetCommentDTO>(comment);
        //}

        public async Task<ICollection<GetCommentDTO>> GetByPostId(int postId)
        {
            var comments = await _commentsRepository.GetByPostId(postId);
            return _mapper.Map<ICollection<GetCommentDTO>>(comments);
        }

        //ToDo
        public async Task Like(int userId, int commentId)
        {
            if (await _commentsRepository.HasUserDislikedComment(userId, commentId))
            {
                await _commentsRepository.RemoveDislike(userId, commentId);
            }
            await _commentsRepository.Like(userId, commentId);
        }

        public async Task RemoveLike(int userId, int commentId)
        {
            await _commentsRepository.RemoveLike(userId, commentId);
        }

        public async Task Dislike(int userId, int commentId)
        {
            if (await _commentsRepository.HasUserLikedComment(userId, commentId))
            {
                await _commentsRepository.RemoveLike(userId, commentId);
            }
            await _commentsRepository.Dislike(userId, commentId);
        }

        public async Task RemoveDislike(int userId, int commentId)
        {
            await _commentsRepository.RemoveDislike(userId, commentId);
        }

        public async Task<GetCommentDTO> Block(UserFromTokenDTO userFromToken , int id)
        {

            if (userFromToken.Role != "Admin")
            {
                throw new UnauthorizedAccessException(BlockErrorMessage);
            }
            //var commentToBlock = await this.Get(id);
            var commentToBlock = await this._commentsRepository.Block(id);
            return this._mapper.Map<GetCommentDTO>(commentToBlock);
        }

        public async Task<GetCommentDTO> Unblock(UserFromTokenDTO userFromToken, int id)
        {
            if (userFromToken.Role != "Admin")
            {
                throw new UnauthorizedAccessException(BlockErrorMessage);
            }
            //var commentToBlock = await this.GetBlocked(id)
            var commentToBlock = await this._commentsRepository.UnBlock(id);
            return this._mapper.Map<GetCommentDTO>(commentToBlock);
        }
        public async Task<ICollection<Comment>> Search(QueryParameters parameters, FilterParameters? filterParameters = null)
        {
            var comments = await _commentsRepository.Search(parameters, filterParameters);
            return this._mapper.Map<ICollection<Comment>>(comments);
        }

        public async Task<bool> CheckNext(QueryParameters parameters, FilterParameters? filterParameters = null)
        {
            return await _commentsRepository.CheckNext(parameters, filterParameters);
        }
        public async Task<bool> HasUserLikedComment(int userId, int commentId)
        {
            return await _commentsRepository.HasUserLikedComment(userId, commentId);
        }
        public async Task<bool> HasUserDislikedComment(int userId, int commentId)
        {
            return await _commentsRepository.HasUserDislikedComment(userId, commentId);
        }
        public async Task<ICollection<Comment>> GetPostComments(int postId)
        {
            return await _commentsRepository.GetPostComments(postId);
        }

        public async Task<ICollection<Comment>> GetReplies(int commentId)
        {
            return await _commentsRepository.GetReplies(commentId);
        }

        public async Task<bool> HasReplies(int commentId)
        {
            return await _commentsRepository.HasReplies(commentId);
        }
    }
}
