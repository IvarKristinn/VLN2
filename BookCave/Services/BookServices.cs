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

        public List<BookThumbnailViewModel> GetBooksById()
        {
            var books = _bookRepo.GetBooksById();
            return books;
        }

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
        public List<BookThumbnailViewModel> GetTopTenBooks()
        {
            var topTenBooks  = _bookRepo.GetTopTenBooks();
            return topTenBooks;
        }

        public bool UpdateBookRating(int bookId, int rating)
        {
            return _bookRepo.UpdateBookRating(bookId, rating);
        }

        public bool AddReview(string userId, int bookId, string review)
        {
            return _bookRepo.AddReview(userId, bookId, review);
        }
        
        public List<BookThumbnailViewModel> GetAffordableBooks()
        {
            var affordableBooks = _bookRepo.GetAffordableBooks();
            return affordableBooks;
        }
    }
}