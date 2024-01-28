using Forum_Management_System.Models;

namespace Forum_Management_System.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        Task<ICollection<User>> GetAll(UserQueryParameters parameters);
        Task<User> GetUserByUsername(string username);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByPhone(string phone);
        Task<User> GetUserByID(int? id);
        Task<User> Create(User user);
        Task<User> Update(User userToUpdate, User userChanges);
        Task<User> Delete(string userName);
        Task<User> Block(string userName);
        Task<User> Unblock(string userName);
        Task<ICollection<User>> Search(UserQueryParameters parameters, FilterParameters? filterParameters = null);
        Task<bool> CheckNext(UserQueryParameters parameters, FilterParameters? filterParameters = null);
        public void DecreaseKarma(User user);
        public void IncreaseKarma(User user);
        Task<int> GetCommentCount(int userId);
        Task<int> GetPostsCount(int userId);

        public Task AddImage(string email, byte[] image);
        public Task AddTwitter(User user);
        public Task AddFacebook(User user);
        public Task AddInstagram(User user);
    }
}