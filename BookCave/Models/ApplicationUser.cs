using Microsoft.AspNetCore.Identity;

namespace BookCave.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string ProfilePicLink { get; set; }
        public int FavBookId { get; set; }
    }
}