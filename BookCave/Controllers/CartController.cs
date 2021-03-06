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
        [Authorize]
        public IActionResult BillingAndShipping(BillingAndShippingInputModel newAddresses)
        {
            if(ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                _cartService.AddTempAddresses(newAddresses, userId);
                return RedirectToAction("BillingAndShippingConfirmation");
            }
            return View();
        }

        public IActionResult BillingAndShippingConfirmation()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var addresses = _cartService.GetTempAddressesById(userId);
            if(addresses == null)
            {
                return RedirectToAction("PurchaseError");
            }
            return View(addresses);
        }

        public IActionResult PurchaseError()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _cartService.RemoveAllTempAddressesFromThisUser(userId);
            return View();
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

            /* 
             * Probably should send this down to the service layer,
             * but I just dont think its worth it for such a small 
             * entity model creation
             */
            var order = new Order 
                        {
                            UserId = userId,
                            ItemGroupingId = _cartService.GetCartItemGroupingId(userId),
                        };
            
            _cartService.AddOrderToHistories(order);
            
            var currCartItems = _cartService.GetCartItemsRaw(userId);

            _cartService.SaveOldCartItems(currCartItems);

            _cartService.RemoveAllCurrentCartItems(userId);

            return View(addresses);
        }
    }
}