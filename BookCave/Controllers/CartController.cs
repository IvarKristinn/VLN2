using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookCave.Models;
using BookCave.Services;
using Microsoft.AspNetCore.Authorization;

namespace BookCave.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private CartService _cartService;
        public CartController()
        {
            _cartService = new CartService();
        }
        public IActionResult CartView(string id)
        {
            var ThisCartsItems = _cartService.GetCartItems(id);
            return View();
        }
        
        public void AddToCart(int id)
        {
            return;
        }
        
        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}