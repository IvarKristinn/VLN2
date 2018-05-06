using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookCave.Models;
using BookCave.Services;
using BookCave.Models.ViewModels;

namespace BookCave.Controllers
{
    public class BookController : Controller
    {
        private BookService _bookService;
        public BookController()
        {
            _bookService = new BookService();
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
                return View(bookByGenre);
            }
            return View("NotFound");
        }


        ////what the hell 
        public IActionResult Search(string search)
        {
            var searchBooks = _bookService.GetSearchString(search);
            if(searchBooks != null)
            {
                return View(searchBooks);
            }
            return View("NotFound");

        }

        [HttpPost]
        public IActionResult Review(int rating, string review)
        {
            //Create a new database for comments (Id, BookId, UserId, UserReview)
            //send review into that database as new review
            //Add rating to this book, make new rating avg calcualtions and numRatings++
            //might need to add asp-route-bookid in the view as a third parameter
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}