using System.ComponentModel.DataAnnotations;

namespace Forum_Management_System.Models.DTO
{
    public class GetPostDTO
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public ICollection<TagDTO> Tags { get; set; }
        public ICollection<GetCommentDTO> Comments { get; set; }
        public bool IsBlocked { get; set; }
    }
}
