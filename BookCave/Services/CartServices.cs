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
        public void AddBookToCart(int bookId, string userId, int quantity)
        {
            _tempRepo.AddBookToCart(bookId, userId, quantity);
        }

        public void RemoveBookFromCart(int bookId, string userId)
        {
            _tempRepo.RemoveBookFromCart(bookId, userId);
        }

        public List<CartItemsViewModel> GetCartItems(string id)
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