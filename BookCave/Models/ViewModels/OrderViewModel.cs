using System.Collections.Generic;
using BookCave.Data.EntityModels;

namespace BookCave.Models.ViewModels
{
    public class OrderViewModel
    {
        public List<CartItemsViewModel> OrderItems { get; set; }
        public Address Billing { get; set; }
        public Address Shipping { get; set; }
    }
}