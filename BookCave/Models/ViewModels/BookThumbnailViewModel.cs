namespace BookCave.Models.ViewModels
{
    public class BookThumbnailViewModel
    {
        private int _id { get; set; }
        public string title { get; set; }
        public string auothor { get; set; }
        public string imageLink { get; set; }
        public int price { get; set; }
        public int userRatingAvg { get; set; }
    }
}