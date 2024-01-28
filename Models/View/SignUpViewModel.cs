using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Forum_Management_System.Models.View
{
    public class SignUpViewModel
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

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "The {0} provided should be between {2} and {1} characters long.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "The {0} provided should be between {2} and {1} characters long.")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Remote("IsEmailUnique", "Validator")]
        [StringLength(EmailMaxLenght, MinimumLength = EmailMinLenght, ErrorMessage = "The {0} provided should be between {2} and {1} characters long.")]
        public string Email { get; set; }

        [Required]
        
        [StringLength(UsernameMaxLength, MinimumLength = UsernameMinLength, ErrorMessage = "The {0} provided should be between {2} and {1} characters long.")]
        [Remote("IsUsernameUnique", "Validator")]
        public string Username { get; set; }

        [Required]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength, ErrorMessage = "The {0} provided should be between {2} and {1} characters long.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength, ErrorMessage = "The {0} provided should be between {2} and {1} characters long.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string RepeatedPassword { get; set; }

        [StringLength(PhoneMaxLenght, MinimumLength = PhoneMinLenght, ErrorMessage = "The {0} provided should be between {2} and {1} digits long.")]
        [RegularExpression("^\\d{7,15}$", ErrorMessage = "The {0} provided should be between 7 and 14 digits long.")]
        [Remote("IsPhoneUnique", "Validator")]
        public string PhoneNumber { get; set; }

        public string Role = "User";
    }
}
