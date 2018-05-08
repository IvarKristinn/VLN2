using System.Collections.Generic;

namespace BookCave.Models.ViewModels
{
    public class FrontPageViewModel
    {
        public List <BookThumbnailViewModel> TopTwelve { get; set; }
        public List <BookThumbnailViewModel> NewestBooks { get; set; }

    }
}