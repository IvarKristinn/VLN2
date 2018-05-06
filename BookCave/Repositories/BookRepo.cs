using System.Collections.Generic;
using BookCave.Data;
using BookCave.Models.ViewModels;
using System.Linq;

namespace BookCave.Repositories
{
    public class BookRepo
    {
        private DataContext _db;

        public BookRepo()
        {
            _db = new DataContext();
        }

        public List<BookThumbnailViewModel> GetBooksByTitle()
        {
            var books = (from b in _db.Books
                         select new BookThumbnailViewModel
                        {
                            Id = b.Id,
                            Title = b.Title,
                            Author = b.Author,
                            Price = b.Price,
                            ImageLink = b.ImageLink,
                            UserRatingAvg = b.UserRatingAvg
                        }).ToList();
            return books;
        }

        public BookDetailsViewModel GetBookDetailsById(int id)
        {
        var book = (from b in _db.Books
                    where b.Id == id
                    select new BookDetailsViewModel
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Author = b.Author,
                        Description = b.Description,
                        ImageLink = b.ImageLink,
                        Genre = b.Genre,
                        UserRatingAvg = b.UserRatingAvg,
                        NumberOfUserRatings = b.NumberOfUserRating,
                        Price = b.Price
                    }).SingleOrDefault();
        return book;
        }
    }
}