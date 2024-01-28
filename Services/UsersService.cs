using AutoMapper;
using Forum_Management_System.Exceptions;
using Forum_Management_System.Models;
using Forum_Management_System.Models.DTO;
using Forum_Management_System.Models.View;
using Forum_Management_System.Repositories;
using Forum_Management_System.Repositories.Interfaces;
using Forum_Management_System.Services.Interfaces;
using System.Web.Helpers;

namespace Forum_Management_System.Services
{
    public class UsersService : IUsersService
    {
        private const string BlockErrorMessage = "Only admin can block users!";

        private readonly IUsersRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersService(IUsersRepository repository, IMapper mapper)
        {
            this._userRepository = repository;
            this._mapper = mapper;
        }

        public async Task<ICollection<GetUserDTO>> GetAll(UserQueryParameters parameters)
        {
            ICollection<User> retrievedUsers = await this._userRepository.GetAll(parameters);
            retrievedUsers = retrievedUsers.Where(u => !u.IsBlocked).ToList();
            ICollection<GetUserDTO> users = this._mapper.Map<List<GetUserDTO>>(retrievedUsers);

            return users;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            User user = await this._userRepository.GetUserByUsername(username);
            if (user is null)
            {
                throw new EntityNotFoundException("The user with the provided username was not found.");
            }
            if (user.IsBlocked)
            {
                throw new BlockedUserException("This user is blocked!");
            }

            return user;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            User user = await this._userRepository.GetUserByEmail(email);
            if (user is null)
            {
                throw new EntityNotFoundException("The user with the provided email was not found.");
            }

            return user;
        }

        public async Task AddImage(string email, byte[] image)
        {
            await this._userRepository.AddImage(email, image);
        }
        public async Task AddTwitterAccount(string email, string twitterAccount)
        {
            User user = await GetUserByEmail(email);
            user.TwitterProfile = twitterAccount;
            await this._userRepository.AddTwitter(user);
        }
        public async Task AddInstagramAccount(string email, string instagramAccount)
        {
            User user = await GetUserByEmail(email);
            user.InstagramProfile = instagramAccount;
            await this._userRepository.AddTwitter(user);
        }
        public async Task AddFacebookAccount(string email, string facebookAccount)
        {
            User user = await GetUserByEmail(email);
            user.FacebookProfile = facebookAccount;
            await this._userRepository.AddFacebook(user);
        }

        public async Task<User> GetUserByID(int? id)
        {
            User user = await this._userRepository.GetUserByID(id);
            if (user is null)
            {
                throw new EntityNotFoundException("The user with the provided ID was not found.");
            }
            if (user.IsBlocked)
            {
                throw new BlockedUserException("This user is blocked!");
            }

            return user;
        }

        public async Task<User> GetUserByIDBlocked(int? id)
        {
            User user = await this._userRepository.GetUserByID(id);
            if (user is null)
            {
                throw new EntityNotFoundException("The user with the provided ID was not found.");
            }
            return user;
        }
        public async Task<GetUserDTO> Create(CreateUserDTO userDTO)
        {
            await CheckUsernameUniqueness(userDTO.Username);

            await CheckEmailUniqueness(userDTO.Email);

            User userToCreate = this._mapper.Map<User>(userDTO);
            userToCreate.Password = Crypto.HashPassword(userToCreate.Password);
            userToCreate = await this._userRepository.Create(userToCreate);

            return this._mapper.Map<GetUserDTO>(userToCreate);
        }

        public async Task<bool> CheckPhoneUniqueness(string username)
        {
            User user = await this._userRepository.GetUserByPhone(username);
            if (user != null)
            {
                throw new DuplicateEntityException($"The phone number provided is already registered. Please use a different phone number.");
            }

            return true;
        }

        public async Task<bool> CheckUsernameUniqueness(string username)
        {
            User user = await this._userRepository.GetUserByUsername(username);
            if (user != null)
            {
                throw new DuplicateEntityException($"Username already taken. Please choose a different username.");
            }

            return true;
        }
        public async Task<bool> CheckEmailUniqueness(string email)
        {
            bool isUnique = false;
            User user = await this._userRepository.GetUserByEmail(email);
            if (user == null)
            {
                isUnique = true;
            }

            if (!isUnique)
            {
                throw new DuplicatedEmailException("Email already in use. Please use a different email address for registration.");
            }

            return isUnique;
        }

        public async Task<GetUserDTO> Delete(string username)
        {
            User deletedUser = await this._userRepository.Delete(username);

            return this._mapper.Map<GetUserDTO>(deletedUser);
        }

        public async Task<GetUserDTO> Update(string email, User userChanges)
        {
            User userToUpdate = await GetUserByEmail(email);
            userToUpdate = await this._userRepository.Update(userToUpdate, userChanges);

            return this._mapper.Map<GetUserDTO>(userToUpdate);
        }

        public async Task<GetUserDTO> Block(User user, UserFromTokenDTO admin)
        {

            if (!(admin.Role == "Admin"))
            {
                throw new UnauthorizedAccessException(BlockErrorMessage);
            }
            User userToBlock = await GetUserByUsername(user.Username);
            userToBlock = await this._userRepository.Block(user.Username);

            return this._mapper.Map<GetUserDTO>(userToBlock);
        }

        public async Task<GetUserDTO> Unblock(User user, UserFromTokenDTO admin)
        {

            if (!(admin.Role == "Admin"))
            {
                throw new UnauthorizedAccessException(BlockErrorMessage);
            }
            //User userToUnBlock = await GetUserByUsername(user.Username);
            User userToUnBlock = await this._userRepository.Unblock(user.Username);

            return this._mapper.Map<GetUserDTO>(userToUnBlock);
        }

        public async Task<ICollection<UserViewModelMini>> Search(UserQueryParameters parameters, FilterParameters? filterParameters = null)
        {
            var users = await _userRepository.Search(parameters);
            return this._mapper.Map<ICollection<UserViewModelMini>>(users);
        }

        public async Task<bool> CheckNext(UserQueryParameters paramters, FilterParameters? filterParameters = null)
        {
            return await _userRepository.CheckNext(paramters);
        }
        public Task<int> GetCommentCount(int userId)
        {
            return _userRepository.GetCommentCount(userId);
        }

        public Task<int> GetPostsCount(int userId)
        {
            return _userRepository.GetPostsCount(userId);
        }
    }
}
