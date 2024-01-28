using System.ComponentModel.DataAnnotations;

namespace Forum_Management_System.Models
{
    public class Post
    {
        public Post()
        {
            this.Likes = new HashSet<PostLike>();
            this.Dislikes = new HashSet<PostDislike>();
            this.Tags = new HashSet<Tag>();
            this.Comments = new HashSet<Comment>();
            this.PostTag = new HashSet<PostTag>();
        }

        public int ID { get; set; }
        public DateTime CreatedAt { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} field is required and must not be an empty string.")]
        [MaxLength(64, ErrorMessage = "The {0} field must be less than {1} characters.")]
        [MinLength(4, ErrorMessage = "The {0} field must be at least {1} character.")]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} field is required and must not be an empty string.")]
        [MaxLength(8192, ErrorMessage = "The {0} field must be less than {1} characters.")]
        [MinLength(32, ErrorMessage = "The {0} field must be at least {1} character.")]
        public string Content { get; set; }
        public int? UserID { get; set; }
        public User User { get; set; }
        public int LikesCount { get; set; }
        public ICollection<PostLike> Likes { get; set; }
        public ICollection<PostDislike> Dislikes { get; set; }
        public ICollection<PostTag> PostTag { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public bool IsBlocked { get; set; }
        public Group Group { get; set; }
        public int? GroupID { get; set; }
    }
}
