using Forum_Management_System.Models;

namespace Forum_Management_System.Services.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<TokenResponse> RequestToken(string email, string password);
        public Task<User> TryGetUser(string email, string password);
        public bool ValidatePassword(User user, string password);
    }
}
