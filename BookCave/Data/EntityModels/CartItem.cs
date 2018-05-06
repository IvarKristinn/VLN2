namespace BookCave.Data.EntityModels
{
    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int Quantity { get; set; }
        public int BookId { get; set; }
        public Book book { get; set; }
    }
}