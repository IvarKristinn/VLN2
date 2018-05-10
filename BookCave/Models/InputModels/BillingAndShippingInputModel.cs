using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.InputModels
{
    public class BillingAndShippingInputModel
    {
        [Required(ErrorMessage = "Please enter a billing street.")]
        [MaxLength(50)]
        public string BillingStreet { get; set; }

        [Required(ErrorMessage = "Please enter a billing house number.")]
        [Range(0,9999)]
        public int BillingHouseNum { get; set; }

        [Required(ErrorMessage = "Please enter a billing city.")]
        [MaxLength(50)]
        public string BillingCity { get; set; }

        [Required(ErrorMessage = "Please choose a billing country")]
        [MaxLength(50)]
        public string BillingCountry { get; set; }

        [Required(ErrorMessage = "Please enter a billing ZipCode.")]
        [Range(1,9999)]
        public int BillingZipCode { get; set; }

                [Required(ErrorMessage = "Please enter a shipping street.")]
        [MaxLength(50)]
        public string ShippingStreet { get; set; }

        [Required(ErrorMessage = "Please enter a shipping house number.")]
        [Range(0,9999)]
        public int ShippingHouseNum { get; set; }

        [Required(ErrorMessage = "Please enter a shipping city.")]
        [MaxLength(50)]
        public string ShippingCity { get; set; }

        [Required(ErrorMessage = "Please choose a shipping country")]
        [MaxLength(50)]
        public string ShippingCountry { get; set; }

        [Required(ErrorMessage = "Please enter a shipping ZipCode.")]
        [Range(1,9999)]
        public int ShippingZipCode { get; set; }
    }
}