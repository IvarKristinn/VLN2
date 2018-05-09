using System.Collections.Generic;
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
    }
}