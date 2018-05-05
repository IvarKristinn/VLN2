namespace BookCave.Models.ViewModels
{
    public class BookDetailsViewModel
    {
        private int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string ImageLink { get; set; }
        public string Genre { get; set; }
        public int UserRatingAvg { get; set; }
        public int NumberOfUserRatings { get; set; }
        public double Price { get; set; }
        //public List<CommentId>;

    }
}