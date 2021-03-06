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

        public void RemoveAllCurrentCartItems(string userId)
        {
            _dbRepo.RemoveAllCurrentCartItems(userId);
        }

        public void SaveOldCartItems(List<CartItem> cartItems)
        {
            List<OldCartItem> oldCartItems = new List<OldCartItem>();

            foreach(var ci in cartItems)
            {
                var old = new OldCartItem
                {
                    CartId = ci.CartId,
                    GroupingId = _dbRepo.GetCartItemGroupingId(ci.CartId),
                    Quantity = ci.Quantity,
                    BookId = ci.BookId
                };
                oldCartItems.Add(old);
            }

            _dbRepo.SaveOldCartItems(oldCartItems);
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

        public void RemoveAllTempAddressesFromThisUser(string userId)
        {
            _dbRepo.RemoveAllTempAddressesFromThisUser(userId);
        }

        public void AddOrderToHistories(Order order)
        {
            _dbRepo.AddOrderToHistories(order);
        }
    }
}