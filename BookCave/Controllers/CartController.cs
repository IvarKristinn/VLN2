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
            //GetUsersSavedAddresses if not null
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

        public IActionResult Receipt()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var addresses = _cartService.GetTempAddressesById(userId);
            //_cartService.AddAddressesToOrder(addresses, userId);
            _cartService.RemoveAddressesFromTemp(userId);
            //Create new Order entity model with current CartItems, this orders billing and shipping addresses
            /* 
            var order = new Order 
                        {
                            UserId = userId,
                            OrderNum = orderNum,
                            OrderItems = _cartService.GetCartItems(userId),
                            Billing = BillShipp[0],
                            Shipping = BillShipp[1]
                        };
            */
            //_cartService.AddOrderToHistories(order);
            //Delete CartItems from ShoppingCartItem database
            return View();
        }
    }
}