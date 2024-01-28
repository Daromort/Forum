using Forum_Management_System.Models;
using Forum_Management_System.Models.DTO;

namespace Forum_Management_System.Repositories.Interfaces
{
    public interface ICommentsRepository
    {
        Task<Comment> Create(Comment comment);
        Task<Comment> Update(Comment comment);
        Task Delete(Comment comment);
        Task<Comment> Get(int id);
        Task<ICollection<Comment>> GetByPostId(int postId);
        Task LoadRepliesRecursively(ICollection<Comment> comments);
        Task LoadRepliesRecursively(Comment comment);
        Task Like(int userId, int commentId);
        Task RemoveLike(int userId, int commentId);
        Task Dislike(int userId, int commentId);
        Task RemoveDislike(int userId, int commentId);
        Task<Comment> Block(int id);
        Task<Comment> UnBlock(int id);
        Task<ICollection<Comment>> Search(QueryParameters parameters, FilterParameters? filterParameters = null);
        Task<bool> CheckNext(QueryParameters parameters, FilterParameters? filterParameters = null);
        Task<bool> HasUserLikedComment(int userId, int commentId);
        Task<bool> HasUserDislikedComment(int userId, int commentId);
        Task<ICollection<Comment>> GetPostComments(int postId);
        Task<ICollection<Comment>> GetReplies(int commentId);
        Task<bool> HasReplies(int commentId);

        //Task<GetCommentDTO> ConstructGetCommentDTO(Comment comment, User user);
        //Task PopulateReplies(ICollection<Comment> comments);
        //Task TransformReplies(ICollection<Comment> comments, ICollection<GetCommentDTO> getCommentDTOs);
        //Task<ICollection<Comment>> GetByParentId(int parentId);
    }
}
