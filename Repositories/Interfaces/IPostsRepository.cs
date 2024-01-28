using Forum_Management_System.Models;
using Forum_Management_System.Models.DTO;

namespace Forum_Management_System.Repositories.Interfaces
{
    public interface IPostsRepository
    {
        Task Save(Post post);
        Task<ICollection<Post>> Get(PostQueryParameters parameters);
        Task<ICollection<Post>> GetBlocked(PostQueryParameters parameters);
        Task<Post> GetByID(int id);
        Task<Post> Create(CreatePostDTO postDTO, User user);
        Task Delete(Post post);
        Task<Post> Block(int id);
        Task<Post> Unblock(int id);
        Task<ICollection<Post>> Search(PostQueryParameters parameters, FilterParameters? filterParameters = null);
        Task<bool> CheckNext(PostQueryParameters parameters, FilterParameters? filterParameters = null);
        Task<bool> HasUserLikedPost(int userId, int postId);
        Task<bool> HasUserDislikedPost(int userId, int postId);
        Task Like(int userId, int postId);
        Task RemoveLike(int userId, int postId);
        Task Dislike(int userId, int postId);
        Task RemoveDislike(int userId, int postId);
    }
}
