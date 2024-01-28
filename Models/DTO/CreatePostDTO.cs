using System.ComponentModel.DataAnnotations;

namespace Forum_Management_System.Models.DTO
{
    public class CreatePostDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} field is required and must not be an empty string.")]
        [MaxLength(64, ErrorMessage = "The {0} field must be less than {1} characters.")]
        [MinLength(4, ErrorMessage = "The {0} field must be at least {1} character.")]

        public string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} field is required and must not be an empty string.")]
        [MaxLength(64, ErrorMessage = "The {0} field must be less than {1} characters.")]
        [MinLength(8, ErrorMessage = "The {0} field must be at least {1} character.")]

        public string Content { get; set; }

        [Required(ErrorMessage = "The {0} field is required")]
        public HashSet<Tag> Tags { get; set; }
        //public int UserID { get; set; }
    }
}
