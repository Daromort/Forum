using System.ComponentModel.DataAnnotations;

namespace Forum_Management_System.Models.DTO
{
    public class CreateGroupDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} field is required and must not be an empty string.")]
        [MaxLength(64, ErrorMessage = "The {0} field must be less than {1} characters.")]
        [MinLength(4, ErrorMessage = "The {0} field must be at least {1} character.")]

        public string Name { get; set; }
    }
}
