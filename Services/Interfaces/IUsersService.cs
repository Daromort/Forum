using Forum_Management_System.Models;
using Forum_Management_System.Models.DTO;
using Forum_Management_System.Models.View;

namespace Forum_Management_System.Services.Interfaces
{
    public interface IUsersService
    {
        public Task<ICollection<GetUserDTO>> GetAll(UserQueryParameters parameters);
        public Task<User> GetUserByUsername(string username);
        public Task<User> GetUserByEmail(string email);
        public Task<User> GetUserByID(int? id);
        public Task<User> GetUserByIDBlocked(int? id);
        public Task<GetUserDTO> Create(CreateUserDTO user);
        public Task<GetUserDTO> Update(string userEmail, User updatedUser);
        public Task<GetUserDTO> Delete(string username);
        public Task<GetUserDTO> Block(User user, UserFromTokenDTO admin);
        public Task<GetUserDTO> Unblock(User user, UserFromTokenDTO admin);
        public Task<bool> CheckEmailUniqueness(string email);
        public Task<bool> CheckUsernameUniqueness(string username);
        public Task<bool> CheckPhoneUniqueness(string username);
        Task<ICollection<UserViewModelMini>> Search(UserQueryParameters parameters, FilterParameters? filterParameters = null);
        Task<bool> CheckNext(UserQueryParameters parameteres, FilterParameters? filterParameters = null);
        Task<int> GetCommentCount(int userId);
        Task<int> GetPostsCount(int userId);
        public Task AddImage(string email, byte[] image);
        public Task AddTwitterAccount(string email, string twitterAccount);
        public Task AddFacebookAccount(string email, string facebookAccount);
        public Task AddInstagramAccount(string email, string instagramAccount);
    }
}
