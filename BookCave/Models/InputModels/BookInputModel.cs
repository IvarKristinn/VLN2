namespace BookCave.Models.InputModels
{
    public class BookInputModel
    {
        public string Title { get; set; }
        public string ImageLink { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public double Price { get; set; }
        public string Author { get; set; }
    }
}