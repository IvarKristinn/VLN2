using System.Collections.Generic;
using BookCave.Data.EntityModels;
using BookCave.Models.ViewModels;
using BookCave.Repositories;

namespace BookCave.Services
{
    public class CartService
    {
        private DbRepo _dbRepo;
        public CartService()
        {
            _dbRepo = new DbRepo();
        }

        public void AddBookToCart(int bookId, string userId, int quantity)
        {
            _dbRepo.AddBookToCart(bookId, userId, quantity);
        }

        public void RemoveBookFromCart(int bookId, string userId)
        {
            _dbRepo.RemoveBookFromCart(bookId, userId);
        }

        public List<CartItemsViewModel> GetCartItems(string userId)
        {
            var cartItems = _dbRepo.GetCartItems(userId);
            return cartItems;
        }

        public BookDetailsViewModel GetBookDetailsById(int id)
        {
            var book = _dbRepo.GetBookDetailsById(id);
            return book;
        }
    }
}