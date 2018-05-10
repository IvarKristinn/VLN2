using System.Collections.Generic;
using BookCave.Data.EntityModels;
using BookCave.Models.InputModels;
using BookCave.Models.ViewModels;
using BookCave.Repositories;

namespace BookCave.Services
{
    public class AccountService
    {
        private DbRepo _dbRepo;

        public AccountService()
        {
            _dbRepo = new DbRepo();
        }

        public BookThumbnailViewModel GetUserFavBook(int favBookId)
        {
            var book = _dbRepo.GetUserFavBook(favBookId);
            return book;
        }

        public void AddNewAddress(AddressInputModel newAddress, string userId)
        {
            _dbRepo.AddNewAddress(newAddress, userId);
        }

        public void RemoveUserAddress(int addressId, string userId)
        {
            _dbRepo.RemoveUserAddress(addressId, userId);
        }

        public List<AddressViewModel> GetUserAddresses(string userId)
        {
            var addresses = _dbRepo.GetUserAddresses(userId);
            return addresses;
        }

        public List<OrderViewModel> GetOrderHistory(string userId)
        {
            var orders = _dbRepo.GetOrderHistory(userId);
            return orders;
        }
        public void AddNewBook(Book book)
        {
            _dbRepo.AddNewBook(book);
        }
        public List<BookDetailsViewModel> GetSearchString(string search)
        {
            var books = _dbRepo.GetSearchStringDetails(search);
            return books;
        }
    }
}