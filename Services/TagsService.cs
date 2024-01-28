using AutoMapper;
using Forum_Management_System.Exceptions;
using Forum_Management_System.Models;
using Forum_Management_System.Models.DTO;
using Forum_Management_System.Repositories.Interfaces;
using Forum_Management_System.Services.Interfaces;

namespace Forum_Management_System.Services
{
    public class TagsService : ITagsService
    {
        private readonly ITagsRepository _tagsRepository;
        private readonly IMapper _mapper;

        public TagsService(ITagsRepository tagsRepository, IMapper mapper)
        {
            _tagsRepository = tagsRepository;
            _mapper = mapper;
        }

        public async Task<TagDTO> Get(string name)
        {
            var tag = await _tagsRepository.Get(name.ToLowerInvariant());

            if (tag == null)
            {
                throw new EntityNotFoundException("Tag not found.");
            }

            return _mapper.Map<TagDTO>(tag);
        }

        public async Task<TagDTO> Get(int id)
        {
            var tag = await _tagsRepository.Get(id);

            if (tag == null)
            {
                throw new EntityNotFoundException("Tag not found.");
            }

            return _mapper.Map<TagDTO>(tag);
        }

        public async Task Create(TagDTO tagDTO)
        {
            tagDTO.Name = tagDTO.Name.ToLowerInvariant();

            var existingTag = await _tagsRepository.Get(tagDTO.Name);
            if (existingTag != null)
            {
                throw new DuplicateEntityException("Tag already exists.");
            }

            var tag = _mapper.Map<Tag>(tagDTO);
            await _tagsRepository.Create(tag);
        }

        public async Task Delete(int id)
        {
            var tag = await _tagsRepository.Get(id);

            if (tag == null)
            {
                throw new EntityNotFoundException("Tag not found.");
            }

            await _tagsRepository.Delete(tag);
        }

        public async Task Delete(string name)
        {
            var tag = await _tagsRepository.Get(name.ToLowerInvariant());

            if (tag == null)
            {
                throw new EntityNotFoundException("Tag not found.");
            }

            await _tagsRepository.Delete(tag);
        }
    }
}
