using System.Collections.Generic;
using BookCave.Data.EntityModels;
using BookCave.Models.ViewModels;
using BookCave.Repositories;

namespace BookCave.Services
{
    public class CartService
    {
        private DbRepo _tempRepo;
        public CartService()
        {
            _tempRepo = new DbRepo();
        }
        public void AddBookToCart(int bookId, string userId)
        {
            _tempRepo.AddBookToCart(bookId, userId);
        }

        public List<BookDetailsViewModel> GetCartItems(string id)
        {
            var cartItems = _tempRepo.GetCartItems(id);
            return cartItems;
        }

        public BookDetailsViewModel GetBookDetailsById(int id)
        {
            var book = _tempRepo.GetBookDetailsById(id);
            return book;
        }
    }
}