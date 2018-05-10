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
        private AccountService _accountService;
        public CartController()
        {
            _cartService = new CartService();
            _accountService = new AccountService();
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
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var addresses = _accountService.GetUserAddresses(userId);

            if(addresses != null) 
            {
                ViewBag.SavedAddresses = addresses;
                ViewBag.AddressCount = addresses.Count();
                return View();
            }

            return View();
        }

        [HttpPost]
        public IActionResult BillingAndShipping(BillingAndShippingInputModel newAddresses)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _cartService.AddTempAddresses(newAddresses, userId);
            return RedirectToAction("BillingAndShippingConfirmation");
        }

        public IActionResult BillingAndShippingConfirmation()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var addresses = _cartService.GetTempAddressesById(userId);

            return View(addresses);
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult OrderConfirmation()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var addresses = _cartService.GetTempAddressesById(userId);

            if(addresses == null)
            {
                return RedirectToAction("Error");
            }

            _cartService.RemoveAddressesFromTemp(userId);

            var order = new Order 
                        {
                            UserId = userId,
                            ItemGroupingId = _cartService.GetCartItemGroupingId(userId),
                            Billing = new Address
                            {
                                UserId = userId,
                                Street = addresses.BillingAddress.Street,
                                HouseNum = addresses.BillingAddress.HouseNum,
                                City = addresses.BillingAddress.City,
                                Country = addresses.BillingAddress.Country,
                                ZipCode = addresses.BillingAddress.ZipCode
                            },
                            Shipping = new Address
                            {
                                UserId = userId,
                                Street = addresses.ShippingAddress.Street,
                                HouseNum = addresses.ShippingAddress.HouseNum,
                                City = addresses.ShippingAddress.City,
                                Country = addresses.ShippingAddress.Country,
                                ZipCode = addresses.ShippingAddress.ZipCode
                            }
                        };
            
            _cartService.AddOrderToHistories(order);
            
            var currCartItems = _cartService.GetCartItemsRaw(userId);

            _cartService.SaveOldCartItems(currCartItems);

            _cartService.RemoveAllCurrentCartItems(userId);

            return View(addresses);
        }
    }
}