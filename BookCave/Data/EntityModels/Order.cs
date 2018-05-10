using System.Collections.Generic;
using BookCave.Models.ViewModels;

namespace BookCave.Data.EntityModels
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ItemGroupingId { get; set; }
    }
}