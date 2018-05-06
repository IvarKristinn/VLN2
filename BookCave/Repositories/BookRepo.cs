using System.Collections.Generic;
using BookCave.Data;
using BookCave.Models.ViewModels;
using System.Linq;
using BookCave.Data.EntityModels;


namespace BookCave.Repositories
{
    public class BookRepo
    {
        private DataContext _db;

        public BookRepo()
        {
            _db = new DataContext();
        }

        public List<BookThumbnailViewModel> GetBooksByTitle()
        {
            var books = (from b in _db.Books
                         select new BookThumbnailViewModel
                        {
                            Id = b.Id,
                            Title = b.Title,
                            Author = b.Author,
                            Price = b.Price,
                            ImageLink = b.ImageLink,
                            UserRatingAvg = b.UserRatingAvg
                        }).ToList();
            return books;
        }

        public BookDetailsViewModel GetBookDetailsById(int id)
        {
        var book = (from b in _db.Books
                    where b.Id == id
                    select new BookDetailsViewModel
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Author = b.Author,
                        Description = b.Description,
                        ImageLink = b.ImageLink,
                        Genre = b.Genre,
                        UserRatingAvg = b.UserRatingAvg,
                        NumberOfUserRatings = b.NumberOfUserRating,
                        Price = b.Price
                    }).SingleOrDefault();
        return book;
        }

        public List<BookThumbnailViewModel> getByGenre(string genre)
        {
            var bookByGenre = (from b in _db.Books
                                    where b.Genre == genre
                                    select new BookThumbnailViewModel
                                    {
                                       Id = b.Id,
                                        Title = b.Genre,
                                        Author = b.Author,
                                        ImageLink = b.ImageLink,
                                        Price = b.Price,
                                        UserRatingAvg = b.UserRatingAvg
                                    }).ToList();    
             return bookByGenre;                          
        }

        ////what the hell 
        public List<BookThumbnailViewModel> GetSearchString(string search)
        {
                var searchBooks = (from b in _db.Books
                                  where b.Title.ToLower().Contains(search.ToLower())
                                  select new BookThumbnailViewModel
                                    {
                                        Id = b.Id,
                                        Title = b.Genre,
                                        Author = b.Author,
                                        ImageLink = b.ImageLink,
                                        Price = b.Price,
                                        UserRatingAvg = b.UserRatingAvg
                            }).ToList();
            return searchBooks;
        }
            
        public List<BookDetailsViewModel> GetCartItems(string id)
        {
            var cartItems = (from c in _db.ShoppingCartItems
                             join b in _db.Books on c.BookId equals b.Id
                             where c.CartId == id
                             select new BookDetailsViewModel
                             {
                                 Id = b.Id,
                                 Title = b.Title,
                                 Author = b.Author,
                                 Description = b.Description,
                                 ImageLink = b.ImageLink,
                                 Genre = b.Genre,
                                 UserRatingAvg = b.UserRatingAvg,
                                 NumberOfUserRatings = b.NumberOfUserRating,
                                 Price = b.Price
                             }).ToList();
            return cartItems;
        }

        public void AddBookToCart(int bookId, string userId)
        {
            var cartItemAdd = new CartItem 
            {
                CartId = userId,
                Quantity = 1,
                BookId = bookId
            };
            _db.ShoppingCartItems.Add(cartItemAdd);

            _db.SaveChanges();
        }
    }
}
