using System.Collections.Generic;
using BookCave.Data;
using BookCave.Models.ViewModels;
using System.Linq;
using BookCave.Data.EntityModels;


namespace BookCave.Repositories
{
    public class DbRepo
    {
        private DataContext _db;

        public DbRepo()
        {
            _db = new DataContext();
        }

        public List<BookThumbnailViewModel> GetBooksByTitle()
        {
            var books = (from b in _db.Books
                        orderby b.Title
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
                        Price = b.Price,
                        Reviews = (from r in _db.Comments
                                    where r.BookId == id
                                    select r.UserReview).ToList()
                    }).SingleOrDefault();
        return book;
        }

        public List<BookThumbnailViewModel> GetBooksById()
        {
            var books = (from b in _db.Books
                         orderby b.Id descending
                         select new BookThumbnailViewModel
                         {
                            Id = b.Id,
                            Title = b.Title,
                            Author = b.Author,
                            Price = b.Price,
                            ImageLink = b.ImageLink,
                            UserRatingAvg = b.UserRatingAvg
                         }).Take(10).ToList();
            return books;
        }
        
        public List<BookThumbnailViewModel> GetTopRatedBooks()
        {
            var topBooks = (from b in _db.Books
                            orderby b.UserRatingAvg descending
                            select new BookThumbnailViewModel
                            {
                                Id = b.Id,
                                Title = b.Title,
                                Author = b.Author,
                                Price = b.Price,
                                ImageLink = b.ImageLink,
                                UserRatingAvg = b.UserRatingAvg
                            }).Take(10).ToList();
                            return topBooks;
        }


        public List<BookThumbnailViewModel> GetByGenre(string genre)
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
                                  where (b.Title.ToLower().Contains(search.ToLower())
                                        || b.Author.ToLower().Contains(search.ToLower())
                                        || b.Genre.ToLower().Contains(search.ToLower()))
                                  select new BookThumbnailViewModel
                                    {
                                        Id = b.Id,
                                        Title = b.Title,
                                        Author = b.Author,
                                        ImageLink = b.ImageLink,
                                        Price = b.Price,
                                        UserRatingAvg = b.UserRatingAvg
                            }).ToList();
            return searchBooks;
        }
         public List<BookThumbnailViewModel> GetAffordableBooks()
        {
            var affordableBooks  = (from b in _db.Books
                            orderby b.Price ascending
                            select new BookThumbnailViewModel
                            {
                            Id = b.Id,
                            Title = b.Title,
                            Author = b.Author,
                            Price = b.Price,
                            ImageLink = b.ImageLink,
                            UserRatingAvg = b.UserRatingAvg

                            }).Take(10).ToList();
                     return affordableBooks;                     
        }

        //Change from BookDetailViewModel, make BookCartViewModel
        public List<CartItemsViewModel> GetCartItems(string id)
        {
            var cartItems = (from c in _db.ShoppingCartItems
                             join b in _db.Books on c.BookId equals b.Id
                             where c.CartId == id
                             select new CartItemsViewModel
                             {
                                 Id = b.Id,
                                 Title = b.Title,
                                 Author = b.Author,
                                 ImageLink = b.ImageLink,
                                 Price = b.Price,
                                 Quantity = c.Quantity
                             }).ToList();
            return cartItems;
        }

       
        public void AddBookToCart(int bookId, string userId, int quantity)
        {
            var cartItemAdd = new CartItem 
            {
                CartId = userId,
                Quantity = quantity,
                BookId = bookId
            };
            _db.ShoppingCartItems.Add(cartItemAdd);

            _db.SaveChanges();
        }

        public void RemoveBookFromCart(int bookId, string userId)
        {
            var cartItemRem = (from c in _db.ShoppingCartItems
                                where c.CartId == userId
                                && c.BookId == bookId
                                select c).FirstOrDefault();
            _db.ShoppingCartItems.Remove(cartItemRem);
            _db.SaveChanges();
        }

        public bool UpdateBookRating(int bookId, int rating)
        {
            var book = (from b in _db.Books
                        where b.Id == bookId
                        select b).SingleOrDefault();

            if(book.NumberOfUserRating != 0)
            {
                book.UserRatingAvg = ((book.UserRatingAvg * book.NumberOfUserRating) + rating) / (book.NumberOfUserRating + 1);
                book.NumberOfUserRating = book.NumberOfUserRating + 1;
            }
            else
            {
                book.UserRatingAvg = rating;
                book.NumberOfUserRating++;
            }

            _db.Books.Update(book);
            _db.SaveChanges();

            return true;
        }

        public bool AddReview(string userId, int bookId, string review)
        {
            var comment = new Comment 
            {
                UserId = userId,
                BookId = bookId,
                UserReview = review
            };

            _db.Comments.Add(comment);

            _db.SaveChanges();

            return true;
        }
    }
}
