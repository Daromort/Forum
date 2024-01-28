using AutoMapper.QueryableExtensions;
using Flurl;
using Flurl.Http;
using Forum_Management_System.Data;
using Forum_Management_System.Models;
using Forum_Management_System.Models.DTO;
using Forum_Management_System.Models.Enums;
using Forum_Management_System.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Web.Helpers;

namespace Forum_Management_System.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ForumDbContext _context;

        public UsersRepository(ForumDbContext context)
        {
            this._context = context;
        }

        public async Task<ICollection<User>> GetAll(UserQueryParameters parameters)
        {
            if (parameters.ID.HasValue)
            {
                return await _context.Users.Where(u => u.ID == parameters.ID).ToListAsync();
            }

            if (!string.IsNullOrEmpty(parameters.Username))
            {
                return await Task.Run(() => _context.Users.Where(u => u.Username == parameters.Username).ToHashSet());
            }

            if (!string.IsNullOrEmpty(parameters.Email))
            {
                return await _context.Users.Where(u => u.Email == parameters.Email).ToListAsync();
            }

            if (!string.IsNullOrEmpty(parameters.FirstName))
            {
                return await _context.Users.Where(u => u.FirstName == parameters.FirstName).ToListAsync();
            }
            else
            {
                return await _context.Users.ToListAsync();
            }
        }
        public async Task<User> GetUserByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> GetUserByID(int? id)
        {
            if (id is null)
            {
                return null;
            }

            return await _context.Users.FirstOrDefaultAsync(u => u.ID == id);
        }
        public async Task<User> GetUserByEmail(string email)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }
        public async Task<User> Update(User userToUpdate, User userChanges)
        {
            if (userToUpdate != null)
            {
                if (userChanges.FirstName != null)
                {
                    userToUpdate.FirstName = userChanges.FirstName;
                }
                if (userChanges.LastName != null)
                {
                    userToUpdate.LastName = userChanges.LastName;
                }
                if (userChanges.Password != null)
                {
                    userToUpdate.Password = Crypto.HashPassword(userChanges.Password);
                }
                if (userChanges.PhoneNumber != null)
                {
                    userToUpdate.PhoneNumber = userChanges.PhoneNumber;
                }
                await _context.SaveChangesAsync();
            }
            return userToUpdate;
        }
        public async Task<User> Delete(string userName)
        {
            User userToDelete = await GetUserByUsername(userName);
            await Task.Run(() => this._context.Remove(userToDelete));

            return userToDelete;
        }
        public async Task<User> Create(User user)
        {
            await this._context.AddAsync(user);
            await Task.Run(() => _context.SaveChangesAsync());

            return user;
        }

        public async Task<User> Block(string username)
        {
            User userToBlock = await GetUserByUsername(username);

            if (userToBlock != null)
            {
                userToBlock.IsBlocked = true;
                await _context.SaveChangesAsync();
            }

            return userToBlock;
        }

        public async Task<User> Unblock(string username)
        {
            User userToBlock = await GetUserByUsername(username);

            if (userToBlock != null)
            {
                userToBlock.IsBlocked = false;
                await _context.SaveChangesAsync();
            }

            return userToBlock;
        }

        public async Task<User> GetUserByPhone(string phone)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phone);

            return user;
        }

        public async Task<ICollection<User>> Search(UserQueryParameters parameters, FilterParameters? filterParameters = null)
        {
            var query = await SearchQuery(parameters, filterParameters);
            return await query.ToListAsync();
        }

        public async Task<IQueryable<User>> SearchQuery(UserQueryParameters parameters, FilterParameters? filterParameters = null)
        {
            IQueryable<User> query = _context.Users;

            // Exclude blocked users
            query = query.Where(u => !u.IsBlocked);

            if (filterParameters != null)
            {
                if (filterParameters.GroupID != null)
                {
                    query = query.Where(u => u.Groups.Any(g => g.ID == filterParameters.GroupID));
                }
            }

            if (!string.IsNullOrEmpty(parameters.Search))
            {
                query = query.Where(u => u.Username.Contains(parameters.Search) || u.FirstName.Contains(parameters.Search));
            }

            if (parameters.Sort == SortBy.Newest)
            {
                query = query.OrderByDescending(u => u.RegisteredAt);
            }
            else if (parameters.Sort == SortBy.Popular)
            {
                query = query.OrderByDescending(u => u.Karma);
            }

            // Pagination
            query = query.Skip((parameters.Page - 1) * parameters.Size).Take(parameters.Size);

            return query;
        }
        public async Task<bool> CheckNext(UserQueryParameters parameters, FilterParameters? filterParameters = null)
        {
            parameters.Page += 1;
            var query = await SearchQuery(parameters, filterParameters);
            var user = await query.FirstOrDefaultAsync();
            return user != null;
        }

        public void IncreaseKarma(User user)
        {
            user.Karma++;
        }

        public void DecreaseKarma(User user)
        {
            user.Karma--;
        }
        public async Task<int> GetCommentCount(int userId)
        {
            return await _context.Comments.CountAsync(c => c.UserID == userId);
        }

        public async Task<int> GetPostsCount(int userId)
        {
            return await _context.Posts.CountAsync(p => p.UserID == userId);
        }

        public async Task AddImage(string email, byte[] image)
        {
            User user = await this.GetUserByEmail(email);
            user.Photo = image;
            await this._context.SaveChangesAsync();
        }
        public async Task AddTwitter(User user)
        {
            await this._context.SaveChangesAsync();
        }
        public async Task AddFacebook(User user)
        {
            await this._context.SaveChangesAsync();
        }
        public async Task AddInstagram(User user)
        {
            await this._context.SaveChangesAsync();
        }
    }
}