using System.Collections.Generic;
using BookCave.Models.ViewModels;
using BookCave.Repositories;

namespace BookCave.Services
{
    public class BookService
    {
        private DbRepo _bookRepo;
        public BookService()
        {
            _bookRepo = new DbRepo();
        }
        public List<BookThumbnailViewModel> GetBooksByTitle()
        {
            var books = _bookRepo.GetBooksByTitle();
            return books;
        }
        public BookDetailsViewModel GetBookDetailsById(int id)
        {
            var book = _bookRepo.GetBookDetailsById(id);
            return book;
        }

        ////what the hell 

        public List<BookThumbnailViewModel>  GetSearchString(string search)
        {
            var searchBooks = _bookRepo.GetSearchString(search);
            return searchBooks;
        }

        public List<BookThumbnailViewModel> GetByGenre(string genre)
        {
            var bookByGenre = _bookRepo.GetByGenre(genre);
            return bookByGenre;
        }
        public List<BookThumbnailViewModel> GetTopRatedBooks()
        {
            var topBooks  = _bookRepo.GetTopRatedBooks();
            return topBooks;

        }
        public List<BookThumbnailViewModel> GetAffordableBooks()
        {
            var affordableBooks = _bookRepo.GetAffordableBooks();
            return affordableBooks;
        }
    }
}