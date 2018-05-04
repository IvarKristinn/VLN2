namespace BookCave.Models.ViewModels
{
    public class BookThumbnailViewModel
    {
        private int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ImageLink { get; set; }
        public int Price { get; set; }
        public int UserRatingAvg { get; set; }
    }
}