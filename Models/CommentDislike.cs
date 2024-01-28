namespace Forum_Management_System.Models
{
    public class CommentDislike
    {
        public int UserID { get; set; }
        public User User { get; set; }
        public int CommentID { get; set; }
        public Comment Comment { get; set; }
    }
}
