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
        /*[HttpGet]
        public IActionResult BillingAndShipping()
        {
            return View();
        }*/
        [HttpGet]
        public IActionResult BillingAndShipping(AddressViewModel newAddress)
        {
            AddressInputModel address = new AddressInputModel();
            address.Street = newAddress.Street;
            address.HouseNum = newAddress.HouseNum;
            address.City = newAddress.City;
            address.Country = newAddress.Country;
            address.ZipCode = newAddress.ZipCode;
            return View(address);
        }
        [HttpPost]
        public IActionResult BillingAndShipping(AddressInputModel newAddress)
        {
            if(ModelState.IsValid) {
                AddressViewModel address = new AddressViewModel();
                address.Street = newAddress.Street;
                address.HouseNum = newAddress.HouseNum;
                address.City = newAddress.City;
                address.Country = newAddress.Country;
                address.ZipCode = newAddress.ZipCode;
                /// addressan sett í database
                return RedirectToAction("BillingAndShippingConfirmation", address);
            }
            return View();
        }

        public IActionResult BillingAndShippingConfirmation(AddressViewModel address)
        {
            
            return View(address);
        }
        public IActionResult BackToBilling(AddressViewModel oldAddress)
        {
            return RedirectToAction("BillingAndShipping", oldAddress);
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}