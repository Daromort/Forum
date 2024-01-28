namespace Forum_Management_System.Models
{
    public class ChatUser
    {
        public int ChatID { get; set; }
        public Chat Chat { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
