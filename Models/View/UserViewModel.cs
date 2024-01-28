namespace Forum_Management_System.Models.View
{
    public class UserViewModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public int Karma { get; set; }
        public int PostsCount { get; set; }
        public int CommentCount { get; set; }
        public string? Description { get; set; }
        public string Photo { get; set; }
        public string? InstagramProfile { get; set; }
        public string? FacebookProfile { get; set; }
        public string? TwitterProfile { get; set; }
    }
}
