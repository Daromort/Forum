using Forum_Management_System.Models;
using Forum_Management_System.Models.DTO;

namespace Forum_Management_System.Repositories.Interfaces
{
    public interface IGroupsRepository
    {
        Task<ICollection<Group>> GetAll();

        Task<ICollection<User>> GetAllUsersInGroup(int id);

        Task<ICollection<Post>> GetAllPostsInGroup(int id);

        Task<Post> AddPost(Post post, User user);

        Task<User> AddUser(User Adduser, User user);

        Task<Group> GetByID(int id);

        Task<Group> Create(CreateGroupDTO groupDTO, User user);
        Task<ICollection<Group>> Search(QueryParameters parameters, FilterParameters? filterParameters = null);
        Task<bool> CheckNext(QueryParameters parameters, FilterParameters? filterParameters = null);
    }
}
