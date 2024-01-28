namespace Forum_Management_System.Models.View
{
    public class PostViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<TagViewModel>? Tags { get; set; }
        public UserViewModelMini User { get; set; }
        public GroupViewModelMini? Group { get; set; }
        public int LikesCount { get; set; }
        public bool HasUserLiked { get; set; }
        public bool HasUserDisliked { get; set; }
    }
}
