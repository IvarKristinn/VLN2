using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.InputModels
{
    public class BookInputModel
    {
        [Required(ErrorMessage = "Please enter a title.")]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter an image link.")]
        public string ImageLink { get; set; }

        [Required(ErrorMessage = "Please enter a description")]
        [MaxLength(360)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please select a genre")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "Please enter a price.")]
        [Range(1,9999)]
        public double Price { get; set; }

        [Required(ErrorMessage = "Please enter an athour.")]
        public string Author { get; set; }
    }
}