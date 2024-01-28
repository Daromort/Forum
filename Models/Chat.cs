namespace Forum_Management_System.Models
{
    public class Chat
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<ChatUser> ChatsUsers { get; set; }
    }
}
