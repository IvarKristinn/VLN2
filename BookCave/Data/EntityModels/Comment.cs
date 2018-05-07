namespace BookCave.Data.EntityModels
{
    public class Comment
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int BookId { get; set; }
        public string UserReview { get; set; }
    }
}