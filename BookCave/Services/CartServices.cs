using System.Collections.Generic;
using BookCave.Data.EntityModels;
using BookCave.Models.InputModels;
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

        public List<CartItem> GetCartItemsRaw(string userId)
        {
            var cartItemsRaw = _dbRepo.GetCartItemsRaw(userId);
            return cartItemsRaw;
        }

        public int GetCartItemGroupingId(string  userId)
        {
            var cartItemGroupingId = _dbRepo.GetCartItemGroupingId(userId);
            return cartItemGroupingId;
        }

        public void RemoveAllCartItems(string userId)
        {
            _dbRepo.RemoveAllCartItems(userId);
        }

        public BookDetailsViewModel GetBookDetailsById(int id)
        {
            var book = _dbRepo.GetBookDetailsById(id);
            return book;
        }

        public void AddTempAddresses(BillingAndShippingInputModel newAddresses, string userId)
        {
            _dbRepo.AddTempAddresses(newAddresses, userId);
        }

        public BillingAndShippingViewModel GetTempAddressesById(string userId)
        {
            var addresses = _dbRepo.GetTempAddressesById(userId);
            return addresses;
        }

        public void RemoveAddressesFromTemp(string userId)
        {
            _dbRepo.RemoveAddressesFromTemp(userId);
        }

        public void AddOrderToHistories(Order order)
        {
            _dbRepo.AddOrderToHistories(order);
        }
    }
}