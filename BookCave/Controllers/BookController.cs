using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookCave.Models;
using BookCave.Services;
using BookCave.Models.ViewModels;
using System.Security.Claims;

namespace BookCave.Controllers
{
    public class BookController : Controller
    {
        private BookService _bookService;
        public BookController()
        {
            _bookService = new BookService();
        }

        public IActionResult AllBooks()
        {
            var book = _bookService.GetBooksByTitle();
            if(book != null)
            {
                return View(book);
            }
            return View("NotFound");
        }
        public IActionResult Details(int id)
        {
            var book = _bookService.GetBookDetailsById(id);
            if(book != null)
            {
                return View(book);
            }
            return View("NotFound");
        }

        public IActionResult Genre(string genre)
        {
            var bookByGenre = _bookService.GetByGenre(genre);
            if(bookByGenre != null)
            {
                ViewBag.Genre = genre;
                return View(bookByGenre);
            }
            return View("NotFound");
        }

        public IActionResult Search(string search)
        {
            var searchBooks = _bookService.GetSearchString(search);
            if(searchBooks != null)
            {
                return View(searchBooks);
            }
            return View("NotFound");
        }

        public IActionResult TopRated()
        {
            var topBooks = _bookService.GetTopRatedBooks();
            if(topBooks != null)
            {
                return View(topBooks);
            }
            return View("NotFound");
        }

        public IActionResult AffordableBooks()
        {
            var affordableBooks = _bookService.GetAffordableBooks();
            if(affordableBooks != null)
            {
                return View(affordableBooks);
            }
            return View("NotFound");
        }

        [HttpPost]
        public IActionResult Review(int bookId, int rating, string review)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool ReviewSuccess = true;

            if(review != null)
            {
                ReviewSuccess = _bookService.AddReview(userId, bookId,  review);
            }

            bool ratingSuccess = _bookService.UpdateBookRating(bookId, rating);

            if(ratingSuccess && ReviewSuccess)
            {
                return RedirectToAction("Details", new { id = bookId});
            }
            else
            {
                return View("NotFound");
            }
            
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}