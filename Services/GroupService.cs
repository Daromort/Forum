using AutoMapper;
using Forum_Management_System.Models;
using Forum_Management_System.Models.DTO;
using Forum_Management_System.Models.View;
using Forum_Management_System.Repositories;
using Forum_Management_System.Repositories.Interfaces;
using Forum_Management_System.Services.Interfaces;

namespace Forum_Management_System.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupsRepository _repository;
        private readonly IMapper _mapper;

        public GroupService(IGroupsRepository groupsRepository, IMapper mapper)
        {
            this._repository = groupsRepository;
            this._mapper = mapper;
        }

        public Task<Group> Create(CreateGroupDTO groupDTO, User user)
        {
            var group = this._repository.Create(groupDTO, user);

            return group;
        }

        public Task<Group> GetById(int id)
        {
            var group = this._repository.GetByID(id);

            return group;
        }

        public async Task<ICollection<GetPostDTO>> GetPosts(int id)
        {
            var posts = this._repository.GetAllPostsInGroup(id).Result;

            return _mapper.Map<List<GetPostDTO>>(posts);

        }

        public async Task<ICollection<GetUserDTO>> GetUsers(int id)
        {
            var users = this._repository.GetAllUsersInGroup(id).Result;

            return _mapper.Map<List<GetUserDTO>>(users);
        }

        public async Task<ICollection<GroupViewModelMini>> Search(QueryParameters parameters, FilterParameters? filterParameters = null)
        {
            var groups = this._repository.Search(parameters).Result;

            return _mapper.Map<ICollection<GroupViewModelMini>>(groups);
        }
        public async Task<bool> CheckNext(QueryParameters parameters, FilterParameters? filterParameters = null)
        {
            return await _repository.CheckNext(parameters);
        }
    }
}
