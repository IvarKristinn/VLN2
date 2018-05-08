using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookCave.Models;
using BookCave.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using BookCave.Data.EntityModels;
using BookCave.Models.InputModels;
using BookCave.Models.ViewModels;

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
        public IActionResult CartView()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var ThisCartsItems = _cartService.GetCartItems(userId);
            return View(ThisCartsItems);
        }
        public IActionResult AddToCart(int bookId, int quantity)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _cartService.AddBookToCart(bookId, userId, quantity);
            return RedirectToAction("Index" ,"Home");
        }

        public IActionResult RemoveFromCart(int bookId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _cartService.RemoveBookFromCart(bookId, userId);
            return RedirectToAction("CartView", "Cart");
        }
        [HttpGet]
        public IActionResult BillingAndShipping()
        {
            return View();
        }
        [HttpPost]
        public IActionResult BillingAndShipping(BillingAndShippingInputModel newAddresses)
        {
            if(newAddresses != null) {
                BillingAndShippingViewModel addresses = new BillingAndShippingViewModel();
                addresses.BillingAddress.Street = newAddresses.BillingAddress.Street;
                addresses.BillingAddress.HouseNum = newAddresses.BillingAddress.HouseNum;
                addresses.BillingAddress.City = newAddresses.BillingAddress.City;
                addresses.BillingAddress.Country = newAddresses.BillingAddress.Country;
                addresses.BillingAddress.ZipCode = newAddresses.BillingAddress.ZipCode;
                addresses.ShippingAddress.Street = newAddresses.ShippingAddress.Street;
                addresses.ShippingAddress.HouseNum = newAddresses.ShippingAddress.HouseNum;
                addresses.ShippingAddress.City = newAddresses.ShippingAddress.City;
                addresses.ShippingAddress.Country = newAddresses.ShippingAddress.Country;
                addresses.ShippingAddress.ZipCode = newAddresses.ShippingAddress.ZipCode;


                return RedirectToAction("BillingAndShippingConfirmation", addresses) ;
            }
            return View();
        }

        public IActionResult BillingAndShippingConfirmation(BillingAndShippingViewModel address)
        {
        
            return View();
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}