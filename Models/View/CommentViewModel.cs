using System.ComponentModel.DataAnnotations;

namespace Forum_Management_System.Models.View
{
    public class CommentViewModel
    {
        public int ID { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Message { get; set; }
        public int LikesCount { get; set; }
        public PostViewModelMini Post { get; set; }
        public UserViewModelMini User { get; set; }
        public bool HasUserLiked { get; set; }
        public bool HasUserDisliked { get; set; }
    }
}
