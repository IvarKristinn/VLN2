using System.Collections.Generic;
using BookCave.Models.ViewModels;
using BookCave.Repositories;

namespace BookCave.Services
{
    public class BookService
    {
        private BookRepo _bookRepo;
        public BookService()
        {
            _bookRepo = new BookRepo();
        }
        public List<BookThumbnailViewModel> GetBooksByTitle()
        {
            var books = _bookRepo.GetBooksByTitle();
            return books;
        }
    }
}