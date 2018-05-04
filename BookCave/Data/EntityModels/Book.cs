namespace BookCave.Data.EntityModels
{
    public class Book
    {
        private int Id { get; set; }
        public string Title { get; set; }
        public string ImageLink { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public int UserRatingAvg { get; set; }
        public int NumberOfUserRating { get; set; }
        public double Price { get; set; }
        public string Author { get; set; }
        //public List<CommentsId> CommentIdList { get; set; }
    }
}