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
            var books =   (from b in _db.Books
                            select new BookThumbnailViewModel
                            {
                                Title = b.Title
                            }).ToList();
            return books;
        }
    }
}