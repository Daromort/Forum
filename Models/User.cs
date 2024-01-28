using Forum_Management_System.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Forum_Management_System.Models
{
    public class User
    {
        private const int PhoneMinLenght = 7;
        private const int PhoneMaxLenght = 14;
        private const int EmailMinLenght = 3;
        private const int EmailMaxLenght = 64;
        private const int PasswordMinLength = 8;
        private const int PasswordMaxLength = 150;
        private const int UsernameMinLength = 4;
        private const int UsernameMaxLength = 25;
        private const int NameMinLength = 4;
        private const int NameMaxLength = 32;
        private const int DescriptionMaxLength = 256;

        public User()
        {
            this.Posts = new HashSet<Post>();
            this.Comments = new HashSet<Comment>();
            this.PostLikes = new HashSet<PostLike>();
            this.PostDislikes = new HashSet<PostDislike>();
            this.ChatsUsers = new HashSet<ChatUser>();
            this.CommentDislikes = new HashSet<CommentDislike>();
            this.CommentLikes = new HashSet<CommentLike>();
            this.Groups = new HashSet<Group>();
            this.CreatedGroups = new HashSet<Group>();
            this.RegisteredAt = DateTime.UtcNow;
        }

        public int ID { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "The {0} provided should be between {1} and {2} characters long.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "The {0} provided should be between {1} and {2} characters long.")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(EmailMaxLenght, MinimumLength = EmailMinLenght, ErrorMessage = "The {0} provided should be between {1} and {2} characters long.")]
        public string Email { get; set; }

        [Required]
        [StringLength(UsernameMaxLength, MinimumLength = UsernameMinLength, ErrorMessage = "The {0} provided should be between {1} and {2} characters long.")]
        public string Username { get; set; }

        [Required]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength, ErrorMessage = "The {0} provided should be between {1} and {2} characters long.")]
        public string Password { get; set; }

        [StringLength(PhoneMaxLenght, MinimumLength = PhoneMinLenght)]
        public string PhoneNumber { get; set; }

        [Required]
        public string Role { get; set; }

        [StringLength(DescriptionMaxLength, ErrorMessage = "The {0} provided can have a maximum length of {1} characters.")]
        public string? Description { get; set; }
        public int Karma { get; set; }

        public string TwitterProfile { get; set; }
        public string FacebookProfile { get; set; }
        public string InstagramProfile { get; set; }
        public bool IsBlocked { get; set; }
        public byte[] Photo { get; set; }
        public DateTime RegisteredAt { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<PostLike> PostLikes { get; set; }
        public ICollection<PostDislike> PostDislikes { get; set; }
        public ICollection<CommentLike> CommentLikes { get; set; }
        public ICollection<CommentDislike> CommentDislikes { get; set; }
        public ICollection<GroupUser> GroupUsers { get; set; }
        public ICollection<ChatUser> ChatsUsers { get; set; }
        public ICollection<Group> Groups { get; set; }
        public ICollection<Group> CreatedGroups { get; set; }
    }
}
