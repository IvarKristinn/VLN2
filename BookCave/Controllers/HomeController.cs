using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookCave.Models;
using BookCave.Services;
using Microsoft.AspNetCore.Authorization;
using BookCave.Models.ViewModels;

namespace BookCave.Controllers
{
    public class HomeController : Controller
    {
        private BookService _bookService;

        public HomeController()
        {
            _bookService = new BookService();
        }

        public IActionResult Index()
        {
            var books = new FrontPageViewModel();
            books.NewestBooks = _bookService.GetBooksById();
            //prufa
            books.TopTwelve = _bookService.GetTopTwelveBooks();
            return View(books);
        }
    
        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
