using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum_Management_System.Models
{
    [Table("Comments")]
    public class Comment
    {
        public Comment()
        {
            CreatedAt = DateTime.UtcNow;
            this.Likes = new HashSet<CommentLike>();
            this.Dislikes = new HashSet<CommentDislike>();
        }

        public int ID { get; set; }
        public DateTime CreatedAt { get; set; }

        [Required]
        [MaxLength(280)]
        public string Message { get; set; }
        public int LikesCount { get; set; }
        public int PostID { get; set; }
        public Post Post { get; set; }
        public int? UserID { get; set; }
        public User User { get; set; }
        public int? ParentID { get; set; }
        public Comment Parent { get; set; }
        public ICollection<Comment> Replies { get; set; }
        public ICollection<CommentLike> Likes { get; set; }
        public ICollection<CommentDislike> Dislikes { get; set; }
        public bool IsBlocked { get; set; }
    }
}
