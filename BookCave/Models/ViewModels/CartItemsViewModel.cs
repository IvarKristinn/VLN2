using System.Collections.Generic;

namespace BookCave.Models.ViewModels
{
    public class CartItemsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ImageLink { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}