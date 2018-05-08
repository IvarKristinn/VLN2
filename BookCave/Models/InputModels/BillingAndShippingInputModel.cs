using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.InputModels
{
    public class BillingAndShippingInputModel
    {
        public AddressInputModel BillingAddress { get; set; }
        public AddressInputModel ShippingAddress { get; set; }
    }
}