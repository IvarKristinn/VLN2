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
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}