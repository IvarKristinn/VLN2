using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.InputModels
{
    public class LoginInputModel
    {
        [Required(ErrorMessage = "Please enter a valid email address.")]
        [MaxLength(70)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}