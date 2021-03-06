using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.InputModels
{
    public class AddressInputModel
    {
        [Required(ErrorMessage = "Please enter a street.")]
        [MaxLength(50)]
        public string Street { get; set; }

        [Required(ErrorMessage = "Please enter a house number.")]
        [Range(0,9999)]
        public int HouseNum { get; set; }

        [Required(ErrorMessage = "Please enter a city.")]
        [MaxLength(50)]
        public string City { get; set; }

        [Required(ErrorMessage = "Please choose a country")]
        [MaxLength(50)]
        public string Country { get; set; }

        [Required(ErrorMessage = "Please enter a ZipCode.")]
        [Range(1,9999)]
        public int ZipCode { get; set; }
    }
}