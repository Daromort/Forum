using AutoMapper;
using Forum_Management_System.Data;
using Forum_Management_System.Models;
using Forum_Management_System.Models.DTO;
using Forum_Management_System.Models.Enums;
using Forum_Management_System.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Forum_Management_System.Repositories
{
    public class GroupRepository : IGroupsRepository
    {

        private readonly ForumDbContext _context;
        private readonly IMapper _mapper;

        public GroupRepository(ForumDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public Task<Post> AddPost(Post post, User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> AddUser(User Adduser, User user)
        {
            throw new NotImplementedException();
        }

        public async Task<Group> Create(CreateGroupDTO groupDTO, User user)
        {
            var group = _mapper.Map<Group>(groupDTO);
            group.Creator = user;
            group.CreatorID = user.ID;

            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            return group;
        }

        public Task<ICollection<Group>> GetAll()
        {
            var groups = _context.Groups.AsQueryable();

            return (Task<ICollection<Group>>)groups;
        }

        public Task<ICollection<Post>> GetAllPostsInGroup(int id)
        {
            var group = this.GetByID(id).Result;

            var posts = group.Posts.AsQueryable();

            return (Task<ICollection<Post>>)posts;

        }

        public Task<ICollection<User>> GetAllUsersInGroup(int id)
        {
            var group = this.GetByID(id).Result;

            var users = group.Users.AsQueryable();

            return (Task<ICollection<User>>)users;
        }

        public async Task<Group> GetByID(int id)
        {
            return _context.Groups.FirstOrDefault(g => g.ID == id);
        }

        public async Task<ICollection<Group>> Search(QueryParameters parameters, FilterParameters? filterParameters = null)
        {
            var query = await SearchQuery(parameters, filterParameters);
            return await query.ToListAsync();
        }
        public async Task<IQueryable<Group>> SearchQuery(QueryParameters parameters, FilterParameters? filterParameters = null)
        {
            IQueryable<Group> query = _context.Groups;

            if (filterParameters != null)
            {
                if (filterParameters.UserID != null)
                {
                    
                }
            }

            if (parameters.Search != null)
            {
                query = query.Where(p => p.Name.Contains(parameters.Search));
            }

            // Pagination
            query = query.Skip((parameters.Page - 1) * parameters.Size).Take(parameters.Size);

            return query;
        }
        public async Task<bool> CheckNext(QueryParameters parameters, FilterParameters? filterParameters = null)
        {
            parameters.Page += 1;
            var query = await SearchQuery(parameters,filterParameters);
            var post = await query.FirstOrDefaultAsync();
            return post != null;
        }
    }
}
