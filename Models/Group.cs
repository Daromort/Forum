using System.ComponentModel;

namespace Forum_Management_System.Models
{
    public class Group
    {
        public Group()
        {
            this.Users = new HashSet<User>();

            this.Posts = new HashSet<Post>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public User Creator { get; set; }
        public int CreatorID { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<GroupUser> GroupsUsers { get; set; }
        public string Description { get; set; }
        public int memberCount { get; set; } = 1;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
