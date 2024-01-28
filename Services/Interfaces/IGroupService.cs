using Forum_Management_System.Models;
using Forum_Management_System.Models.DTO;
using Forum_Management_System.Models.View;

namespace Forum_Management_System.Services.Interfaces
{
    public interface IGroupService
    {
        Task<Group> GetById(int id);

        public Task<ICollection<GetUserDTO>> GetUsers(int id);

        public Task<ICollection<GetPostDTO>> GetPosts(int id);

        Task<Group> Create(CreateGroupDTO groupDTO, User user);
        Task<ICollection<GroupViewModelMini>> Search(QueryParameters parameters, FilterParameters? filterParameters = null);
        Task<bool> CheckNext(QueryParameters parameters, FilterParameters? filterParameters = null);
    }
}
