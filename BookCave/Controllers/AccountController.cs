using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BookCave.Data.EntityModels;
using BookCave.Models;
using BookCave.Models.InputModels;
using BookCave.Models.ViewModels;
using BookCave.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookCave.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private AccountService _accountService;
        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _accountService = new AccountService();
            
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            IdentityResult roleResult;

            var roleExist = await _roleManager.RoleExistsAsync("Staff");

            if (!roleExist)
            {
                roleResult = await _roleManager.CreateAsync(new IdentityRole("Staff"));
            }

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                Name = model.Name
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if(result.Succeeded)
            { 
                //await _userManager.AddToRoleAsync(user, "Staff");
                await _userManager.AddClaimAsync(user, new Claim("Name", model.Name));
                await _signInManager.SignInAsync(user, false);

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public IActionResult Login()
        {
             return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInputModel model)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            if(result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Information()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            var userViewModel = new AccountEditViewModel
                                    {
                                        Name = user.Name,
                                        ProfilePicLink = user.ProfilePicLink,
                                        FavBook = _accountService.GetUserFavBook(user.FavBookId),
                                        UserAddresses = _accountService.GetUserAddresses(userId)
                                    };

            return View(userViewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Information(AccountEditInputModel model, bool removeBook = false)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            
            //Name claim change
            if(ModelState.IsValid)
            {
                user.Name = model.Name;

                //Profile pic change, if user sends in empty or null we give them a default pic
                user.ProfilePicLink = model.ProfilePicLink;

                //Removing fav book if checkbox is ticked
                if(removeBook == true)
                {
                    user.FavBookId = 0;
                }

                await _userManager.UpdateAsync(user);

                return RedirectToAction("Index", "Home");
            }
                
            return View(new AccountEditViewModel {
                Name = user.Name,
                ProfilePicLink = user.ProfilePicLink,
                FavBook = _accountService.GetUserFavBook(user.FavBookId),
                UserAddresses = _accountService.GetUserAddresses(userId)
            });
        }

        [Authorize]
        public async Task<IActionResult> FavoriteThisBook(int bookId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            user.FavBookId = bookId;
            await _userManager.UpdateAsync(user);
            return RedirectToAction("Details", "Book", new { id = bookId});
        }

        [Authorize]
        public async Task<IActionResult> RemoveFavBook()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            user.FavBookId = 0;
            await _userManager.UpdateAsync(user);
            return RedirectToAction("Information", "Account");
        }

        [Authorize]
        public IActionResult Address()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Address(AddressInputModel newAddress)
        {
            if(ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                _accountService.AddNewAddress(newAddress, userId);
                return RedirectToAction("Information");
            }

            return View();
        }

        [Authorize]
        public IActionResult RemoveAddress(int addressId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _accountService.RemoveUserAddress(addressId, userId);
            return RedirectToAction("Information");
        }

        [Authorize]
        public IActionResult History()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = _accountService.GetOrderHistory(userId);
            return View(orders);
        }

        [Authorize(Roles = "Staff")]
        public IActionResult Staff()
        {
            ///Account/AddBook button is on this page
            ///Account/RemoveBook button is on this page
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Staff")]
        public IActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Staff")]
        public IActionResult AddBook(BookInputModel newBook)
        {
            if(ModelState.IsValid)
            {
                var book = new Book
                {
                    Title = newBook.Title,
                    Author = newBook.Author,
                    Description = newBook.Description,
                    Genre = newBook.Genre,
                    Price = newBook.Price,
                    ImageLink = newBook.ImageLink
                };

                _accountService.AddNewBook(book);
                return View("BookAdded");
            }
            return View();
        }
        
        [HttpGet]
        [Authorize(Roles = "Staff")]
        public IActionResult SearchForBook()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Staff")]
        public IActionResult SearchForBook(string search)
        {
            if(search != null)
            {
                return RedirectToAction("RemoveBook", new { search = search });
            }
            return View("NotFound");
        }

        [Authorize(Roles = "Staff")]
        public IActionResult RemoveBook(string search)
        {
            var searchBooks = _accountService.GetSearchString(search);
            if(searchBooks.Count != 0)
            {
                return View(searchBooks);
            }
            ViewBag.SearchString = search;

            return View("BooksNotFound");
        }
        public IActionResult RemoveFromDB(int id)
        {
            _accountService.RemoveBookFromDB(id);

            return View("DeletionConfirmation");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}