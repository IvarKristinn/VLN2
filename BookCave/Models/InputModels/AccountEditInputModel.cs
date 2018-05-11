using System.ComponentModel.DataAnnotations;
using BookCave.Models.ViewModels;

namespace BookCave.Models.InputModels
{
    public class AccountEditInputModel
    {
        [Required(ErrorMessage = "A user must have a name!")]
        public string Name { get; set; }
        public string ProfilePicLink { get; set; }
        public BookThumbnailViewModel FavBook { get; set; }
    }
}