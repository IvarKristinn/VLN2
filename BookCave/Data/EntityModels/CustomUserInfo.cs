namespace BookCave.Data.EntityModels
{
    public class CustomUserInfo
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        //Maybe have name here so you dont need to log out to change info
        public string ImgLink { get; set; }
        public int BookId { get; set; }

        //Address database has userIds to link with AccountViewModel
    }
}