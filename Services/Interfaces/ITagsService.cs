using Forum_Management_System.Models;
using Forum_Management_System.Models.DTO;

namespace Forum_Management_System.Services.Interfaces
{
    public interface ITagsService
    {
        Task<TagDTO> Get(string name);
        Task<TagDTO> Get(int id);
        Task Create(TagDTO tagDTO);
        Task Delete(int id);
        Task Delete(string name);
    }
}
