namespace Forum_Management_System.Models
{
    public  class PostDislike
    {
        public int UserID { get; set; }
        public User User { get; set; }
        public int PostID { get; set; }
        public Post Post { get; set; }
    }
}
 