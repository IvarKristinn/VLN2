using System.Collections.Generic;

namespace BookCave.Models.ViewModels
{
    public class AccountEditViewModel
    {
        public string Name { get; set; }
        public string ProfilePicLink { get; set; }
        public BookThumbnailViewModel FavBook { get; set; }
        public List<AddressViewModel> UserAddresses { get; set; }
    }
}