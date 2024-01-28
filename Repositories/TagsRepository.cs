using System.Formats.Asn1;
using AutoMapper;
using Forum_Management_System.Data;
using Forum_Management_System.Models;
using Forum_Management_System.Models.DTO;
using Forum_Management_System.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Forum_Management_System.Repositories
{

    public class TagsRepository : ITagsRepository
    {
        private readonly ForumDbContext _context;
        private readonly IMapper _mapper;

        public TagsRepository(ForumDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Tag> Get(int id)
        {
            return await _context.Tags.FindAsync(id);
        }

        public async Task<Tag> Get(string name)
        {
            return await _context.Tags.FirstOrDefaultAsync(t => t.Name == name);
        }

        public async Task Create(Tag tag)
        {
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();
        }

        public async Task Create(Tag tag, int postId)
        {
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();

            //ToDo
            var postTag = new PostTag
            {
                PostID = postId,
                TagID = tag.ID
            };

            await _context.PostsTags.AddAsync(postTag);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveFromPost(HashSet<Tag> tags, int postId)
        {
            var postTags = await _context.PostsTags
                .Where(pt => pt.PostID == postId)
                .ToListAsync();

            _context.PostsTags.RemoveRange(postTags);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(Tag tag)
        {
            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
        }
        public async Task<ICollection<Tag>> GetByPostID(int postID)
        {
            var postTags = await _context.PostsTags
                .Where(pt => pt.PostID == postID)
                .Select(pt => pt.Tag)
                .ToListAsync();

            return postTags;
        }

        //ToDo
        //Ask trainers if that is a good idea.
        public async Task<TagDTO> ConstructTagDTO(Tag tag)
        {
            return _mapper.Map<TagDTO>(tag);
        }
    }
}
