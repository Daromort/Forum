using Forum_Management_System.Exceptions;
using Forum_Management_System.Models;
using Forum_Management_System.Models.Enums;
using Forum_Management_System.Repositories.Interfaces;
using Forum_Management_System.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Web.Helpers;

namespace Forum_Management_System.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly IUsersRepository _usersRepository;

        public AuthenticationService(IUsersRepository usersRepository, IConfiguration configuration)
        {
            _usersRepository = usersRepository;
            _configuration = configuration;

        }

        public async Task<TokenResponse> RequestToken(string email, string password)
        {
            User user = await TryGetUser(email, password);
            if (user is null)
            {
                throw new AuthenticationException();
            }

            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub,user.ID.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role),
                };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["Jwt:Key"]));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                    issuer: this._configuration["Jwt:Issuer"],
                    audience: this._configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(30),
                    signingCredentials: credentials
                );

            return new TokenResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
        public async Task<User> TryGetUser(string email, string password)
        {
            try
            {
                User user = await this._usersRepository.GetUserByEmail(email);
                ValidatePassword(user, password);

                return user;
            }
            catch (EntityNotFoundException)
            {
                throw new AuthenticationException("Invalid credentials.");
            }
        }

        public bool ValidatePassword(User user, string password)
        {
            bool isValid = Crypto.VerifyHashedPassword(user.Password, password);

            if (!isValid)
            {
                throw new AuthenticationException("Invalid credentials.");
            }

            return isValid;
        }
    }
}
