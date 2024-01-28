using Forum_Management_System.Models;
using Forum_Management_System.Models.DTO;

namespace Forum_Management_System.Repositories.Interfaces
{
    public interface ITagsRepository
    {
        Task<Tag> Get(int id);
        Task<Tag> Get(string name);
        Task Create(Tag tag);
        Task Create(Tag tag, int postId);
        Task Delete(Tag tag);
        Task<ICollection<Tag>> GetByPostID(int postID);
        Task RemoveFromPost(HashSet<Tag> tags, int postId);
        Task<TagDTO> ConstructTagDTO(Tag tag);
    }
}
