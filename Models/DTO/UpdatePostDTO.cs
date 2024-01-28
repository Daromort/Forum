namespace Forum_Management_System.Models.DTO
{
    public class UpdatePostDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public HashSet<TagDTO> Tags { get; set; }
    }
}
