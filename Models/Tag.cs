using System.ComponentModel.DataAnnotations;

namespace Forum_Management_System.Models
{
    public class Tag
    {
        public Tag()
        {
            this.Posts = new HashSet<Post>();
            this.PostTag = new HashSet<PostTag>();
        }

        public int ID { get; set; }

        [Required]
        [MaxLength(55)]
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<PostTag> PostTag { get; set; }
    }
}
