using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.InputModels
{
    public class RegisterInputModel
    {
        [Required(ErrorMessage = "Please enter a valid email address.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a valid name.")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a valid password")]
        public string Password { get; set; }
    }
}