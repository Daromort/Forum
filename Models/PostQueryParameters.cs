using Forum_Management_System.Models.Enums;

namespace Forum_Management_System.Models
{
    public class PostQueryParameters : QueryParameters
    {
        public string Title { get; set; }
        public string Username { get; set; }
        public ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();
        public PostQueryParameters Clone(int pageIncrement)
        {
            return new PostQueryParameters
            {
                ID = this.ID,
                Title = this.Title,
                Username = this.Username,
                Tags = this.Tags,
                Sort = this.Sort,
                Page = this.Page + pageIncrement,
                Size = this.Size,
                Search = this.Search
            };
        }
    }
}
