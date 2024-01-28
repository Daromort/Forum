using Forum_Management_System.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Forum_Management_System.Models.DTO
{
    public class CreateUserDTO
    {
        private const int EmailMinLenght = 3;
        private const int EmailMaxLenght = 64;
        private const int PasswordMinLength = 8;
        private const int PasswordMaxLength = 150;
        private const int UsernameMinLength = 4;
        private const int UsernameMaxLength = 25;
        private const int NameMinLength = 4;
        private const int NameMaxLength = 32;
        private const int PhoneMinLenght = 7;
        private const int PhoneMaxLenght = 14;

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

        [Required]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength, ErrorMessage = "The {0} provided should be between {1} and {2} characters long.")]
        public string RepeatedPassword { get; set; }

        [StringLength(PhoneMaxLenght, MinimumLength = PhoneMinLenght)]
        public string PhoneNumber { get; set; }

        public string Role { get; set; }
    }
}
