using Forum_Management_System.Models;
using Forum_Management_System.Models.DTO;
using Forum_Management_System.Models.View;

namespace Forum_Management_System.Services.Interfaces
{
    public interface IPostsService
    {
        Task<ICollection<GetPostDTO>> Get(PostQueryParameters parameters);
        Task<ICollection<GetPostDTO>> GetBlocked(PostQueryParameters parameters);
        Task<Post> GetByID(int id);
        Task<GetPostDTO> Create(CreatePostDTO createPostDTO, UserFromTokenDTO userFromToken);
        Task<GetPostDTO> Update(int id, UpdatePostDTO updatePostDTO, UserFromTokenDTO user);
        Task Delete(int id, UserFromTokenDTO user);
        Task Like(int userId, int postId);
        Task RemoveLike(int userId, int postId);
        Task Dislike(int userId, int postId);
        Task RemoveDislike(int userId, int postId);
        Task<GetPostDTO> Block(UserFromTokenDTO user, PostQueryParameters parameters);
        Task<GetPostDTO> Unblock(UserFromTokenDTO user, PostQueryParameters parameters);
        Task<ICollection<Post>> Search(PostQueryParameters parameters, FilterParameters? filterParameters = null);
        Task<bool> CheckNext(PostQueryParameters parameters, FilterParameters? filterParameters = null);
        Task<bool> HasUserLikedPost(int userId, int postId);
        Task<bool> HasUserDislikedPost(int userId, int postId);
    }
}