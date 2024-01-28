using System;

namespace Forum_Management_System.Models
{
    public class Message
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public DateTime Sent { get; set; }
        public int UserID { get; set; }
        public User Sender { get; set; }
        public int ChatID { get; set; }
        public Chat Chat { get; set; }
    }
}
