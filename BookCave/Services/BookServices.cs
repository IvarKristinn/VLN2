using System.Collections.Generic;
using BookCave.Models.ViewModels;
using BookCave.Repositories;

namespace BookCave.Services
{
    public class BookService
    {
        private DbRepo _dbRepo;
        public BookService()
        {
            _dbRepo = new DbRepo();
        }
        public List<BookThumbnailViewModel> GetBooksByTitle()
        {
            var books = _dbRepo.GetBooksByTitle();
            return books;
        }
        public BookDetailsViewModel GetBookDetailsById(int id)
        {
            var book = _dbRepo.GetBookDetailsById(id);
            return book;
        }

        public List<BookThumbnailViewModel> GetBooksById()
        {
            var books = _dbRepo.GetBooksById();
            return books;
        }

        public List<BookThumbnailViewModel>  GetSearchString(string search)
        {
            var searchBooks = _dbRepo.GetSearchString(search);
            return searchBooks;
        }

        public List<BookThumbnailViewModel> GetByGenre(string genre)
        {
            var bookByGenre = _dbRepo.GetByGenre(genre);
            return bookByGenre;
        }
        public List<BookThumbnailViewModel> GetTopRatedBooks()
        {
            var topBooks  = _dbRepo.GetTopRatedBooks();
            return topBooks;

        }
        public List<BookThumbnailViewModel> GetTopTwelveBooks()
        {
            var topTwelveBooks  = _dbRepo.GetTopTwelveBooks();
            return topTwelveBooks;
        }

        public bool UpdateBookRating(int bookId, int rating)
        {
            return _dbRepo.UpdateBookRating(bookId, rating);
        }

        public bool AddReview(string userId, int bookId, string review)
        {
            return _dbRepo.AddReview(userId, bookId, review);
        }
        
        public List<BookThumbnailViewModel> GetAffordableBooks()
        {
            var affordableBooks = _dbRepo.GetAffordableBooks();
            return affordableBooks;
        }
    }
}