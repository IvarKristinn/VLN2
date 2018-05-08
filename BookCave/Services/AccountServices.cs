using System.Collections.Generic;
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
    }
}