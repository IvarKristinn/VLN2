using System.Collections.Generic;
using BookCave.Data.EntityModels;

namespace BookCave.Models.ViewModels
{
    public class OrderViewModel
    {
        public List<CartItemsViewModel> OrderItems { get; set; }
    }
}