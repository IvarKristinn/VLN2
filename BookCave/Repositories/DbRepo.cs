using System.Collections.Generic;
using BookCave.Data;
using BookCave.Models.ViewModels;
using System.Linq;
using BookCave.Data.EntityModels;
using BookCave.Models.InputModels;

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

        public BookThumbnailViewModel GetUserFavBook(int favBookId)
        {
            var book = (from b in _db.Books
                        where b.Id == favBookId
                        select new BookThumbnailViewModel
                        {
                            Id = b.Id,
                            Title = b.Title,
                            Author = b.Author,
                            Price = b.Price,
                            ImageLink = b.ImageLink,
                            UserRatingAvg = b.UserRatingAvg
                        }).FirstOrDefault();
                        
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
                         }).Take(12).ToList();
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
                            }).Take(25).ToList();
                            return topBooks;
        }
        public List<BookThumbnailViewModel> GetTopTwelveBooks()
        {
            var topTwelveBooks = (from b in _db.Books
                            orderby b.UserRatingAvg descending
                            select new BookThumbnailViewModel
                            {
                                Id = b.Id,
                                Title = b.Title,
                                Author = b.Author,
                                Price = b.Price,
                                ImageLink = b.ImageLink,
                                UserRatingAvg = b.UserRatingAvg
                            }).Take(12).ToList();
                            return topTwelveBooks;
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

        public List<BookDetailsViewModel> GetSearchStringDetails(string search)
        {
                var searchBooks = (from b in _db.Books
                                  where (b.Title.ToLower().Contains(search.ToLower())
                                        || b.Author.ToLower().Contains(search.ToLower())
                                        || b.Genre.ToLower().Contains(search.ToLower()))
                                  select new BookDetailsViewModel
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

        public List<CartItemsViewModel> GetCartItems(string userId)
        {
            var cartItems = (from c in _db.ShoppingCartItems
                             join b in _db.Books on c.BookId equals b.Id
                             where c.CartId == userId
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

        public List<CartItem> GetCartItemsRaw(string userId)
        {
            //broken
            var cartItemRaw = (from c in _db.ShoppingCartItems
                               join b in _db.Books on c.BookId equals b.Id
                               where c.CartId == userId
                               select c).ToList();

            return cartItemRaw;
        }

        public void AddBookToCart(int bookId, string userId, int quantity)
        {
            var groupingId = (from c in _db.OldCartItems
                              where c.CartId == userId
                              orderby c.GroupingId descending
                              select c.GroupingId).FirstOrDefault();
            
            if(groupingId == 0)
            {
                groupingId = 1;
            }

            var cartItemAdd = new CartItem 
            {
                CartId = userId,
                GroupingId = groupingId,
                Quantity = quantity,
                BookId = bookId
            };

            _db.ShoppingCartItems.Add(cartItemAdd);

            _db.SaveChanges();
        }

        public int GetCartItemGroupingId(string  userId)
        {
            var cartItemGroupingId = (from c in _db.OldCartItems
                                      where c.CartId == userId
                                      orderby c.GroupingId descending
                                      select c.GroupingId).FirstOrDefault();
            cartItemGroupingId++;
            return cartItemGroupingId;
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

        public void RemoveAllCurrentCartItems(string userId)
        {
            var allUserCartItems = (from c in _db.ShoppingCartItems
                                    where c.CartId == userId
                                    select c).ToList();

            foreach(var item in allUserCartItems)
            {
                _db.ShoppingCartItems.Remove(item);
            }
            _db.SaveChanges();
        }

        public void SaveOldCartItems(List<OldCartItem> oldCartItems)
        {
            foreach(var item in oldCartItems)
            {
                _db.OldCartItems.Add(item);
            }
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

        public void AddNewAddress(AddressInputModel newAddress, string userId)
        {
            var addressEntityModel = new Address()
                                        {
                                            UserId = userId,
                                            Street = newAddress.Street,
                                            HouseNum = newAddress.HouseNum,
                                            City = newAddress.City,
                                            Country = newAddress.Country,
                                            ZipCode = newAddress.ZipCode
                                        };

            _db.Addresses.Add(addressEntityModel);
            _db.SaveChanges();
        }

        public void RemoveUserAddress(int addressId, string userId)
        {
            var address = (from a in _db.Addresses
                           where a.Id == addressId
                           select a).FirstOrDefault();
            
            _db.Addresses.Remove(address);
            _db.SaveChanges();
        }

        public List<AddressViewModel> GetUserAddresses(string userId)
        {
            var addresses = (from a in _db.Addresses
                             where a.UserId == userId
                             select new AddressViewModel
                             {
                                 Id = a.Id,
                                 Street = a.Street,
                                 HouseNum = a.HouseNum,
                                 City = a.City,
                                 Country = a.Country,
                                 ZipCode = a.ZipCode
                             }).ToList();
            
            return addresses;
        }

        public void AddTempAddresses(BillingAndShippingInputModel newAddresses, string userId)
        {
            var billingAddess = new TempAddress 
                                {
                                    UserId = userId,
                                    Street = newAddresses.BillingStreet,
                                    HouseNum = newAddresses.BillingHouseNum,
                                    City = newAddresses.BillingCity,
                                    Country = newAddresses.BillingCountry,
                                    ZipCode = newAddresses.BillingZipCode
                                };
            var shippingAddess = new TempAddress 
                                {
                                    UserId = userId,
                                    Street = newAddresses.ShippingStreet,
                                    HouseNum = newAddresses.ShippingHouseNum,
                                    City = newAddresses.ShippingCity,
                                    Country = newAddresses.ShippingCountry,
                                    ZipCode = newAddresses.ShippingZipCode
                                };

            _db.TempAddresses.Add(billingAddess);
            _db.TempAddresses.Add(shippingAddess);
            _db.SaveChanges();
        }

        public BillingAndShippingViewModel GetTempAddressesById(string userId)
        {
            var addresses = (from t in _db.TempAddresses
                             where t.UserId == userId
                             orderby t.Id descending
                             select t).ToList();
            
            if(addresses.Count() == 2)
            {
                var viewModel = new BillingAndShippingViewModel 
                                {
                                    BillingAddress = new AddressViewModel 
                                                    {
                                                        Street = addresses[1].Street,
                                                        HouseNum = addresses[1].HouseNum,
                                                        City = addresses[1].City,
                                                        Country  = addresses[1].Country,
                                                        ZipCode = addresses[1].ZipCode
                                                    },
                                    ShippingAddress = new AddressViewModel 
                                                    {
                                                        Street = addresses[0].Street,
                                                        HouseNum = addresses[0].HouseNum,
                                                        City = addresses[0].City,
                                                        Country  = addresses[0].Country,
                                                        ZipCode = addresses[0].ZipCode
                                                    }
                                };
                return viewModel;
            }
            else
            {
                return null;
            }
        }

        public void RemoveAddressesFromTemp(string userId)
        {
            var addresses = (from t in _db.TempAddresses
                    where t.UserId == userId
                    orderby t.Id descending
                    select t).ToList();

            _db.TempAddresses.Remove(addresses[0]);
            _db.TempAddresses.Remove(addresses[1]);
            _db.SaveChanges();
        }

        public void AddOrderToHistories(Order order)
        {
            _db.Orders.Add(order);
            _db.SaveChanges();
        }

        public List<OrderViewModel> GetOrderHistory(string userId)
        {
            var orders = (from o in _db.Orders
                          where o.UserId == userId
                          orderby o.Id descending
                          select new OrderViewModel
                          {
                              OrderItems = (from c in _db.OldCartItems
                                            join b in _db.Books on c.BookId equals b.Id
                                            where c.GroupingId == o.ItemGroupingId
                                            && c.CartId == o.UserId
                                            select new CartItemsViewModel
                                                       {
                                                           Id = c.BookId,
                                                           Title = b.Title,
                                                           Author = b.Author,
                                                           ImageLink = b.ImageLink,
                                                           Price = b.Price,
                                                           Quantity = c.Quantity
                                                       }).ToList()
                          }).ToList();
            return orders;
        }

        public void AddNewBook(Book book)
        {
            _db.Books.Add(book);
            _db.SaveChanges();
        }
        public void RemoveBookFromDB(int id)
        {
            var book = (from b in _db.Books
                        where b.Id == id
                        select b).SingleOrDefault();
            _db.Books.Remove(book);
            _db.SaveChanges();
        }
    }
}
