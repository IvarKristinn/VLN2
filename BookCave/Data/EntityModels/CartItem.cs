namespace BookCave.Data.EntityModels
{
    public class CartItem
    {
        public int Id { get; set; }
        public string CartId { get; set; }
        public int GroupingId { get; set; }
        public int Quantity { get; set; }
        public int BookId { get; set; }
    }
}