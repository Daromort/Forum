using Forum_Management_System.Models;
using Forum_Management_System.Models.DTO;

namespace Forum_Management_System.Services.Interfaces
{
    public interface ICommentsService
    {
        Task<GetCommentDTO> Create(CreateCommentDTO createCommentDTO, UserFromTokenDTO user );
        Task<GetCommentDTO> Create(ReplyDTO replyDTO, int parentID, UserFromTokenDTO user );
        Task<GetCommentDTO> Update(int id, UpdateCommentDTO commentDTO, UserFromTokenDTO user );
        Task Delete(int id, UserFromTokenDTO user);
        Task<Comment> Get(int id);
        Task<ICollection<GetCommentDTO>> GetByPostId(int postId);

        Task<GetCommentDTO> Block(UserFromTokenDTO userFromToken, int id);
        Task<GetCommentDTO> Unblock(UserFromTokenDTO userFromToken, int id);
        Task Like(int userId, int commentId);
        Task RemoveLike(int userId, int commentId);
        Task Dislike(int userId, int commentId);
        Task RemoveDislike(int userId, int commentId);
        Task<ICollection<Comment>> Search(QueryParameters parameters, FilterParameters? filterParameters = null);
        Task<bool> CheckNext(QueryParameters parameters, FilterParameters? filterParameters = null);
        Task<bool> HasUserLikedComment(int userId, int commentId);
        Task<bool> HasUserDislikedComment(int userId, int commentId);
        Task<ICollection<Comment>> GetPostComments(int postId);
        Task<ICollection<Comment>> GetReplies(int commentId);
        Task<bool> HasReplies(int commentId);
    }
}
