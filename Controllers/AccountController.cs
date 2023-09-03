using LanguageCourses.Data;
using LanguageCourses.Models;
using LanguageCourses.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LanguageCourses.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }

            var user = await _userManager.FindByEmailAsync(loginVM.Email);

            if (user != null)
            {
                //User was find, now we check password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);

                if (passwordCheck)
                {
                    //In case if password was correct
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Course");
                    }
                }
                //In case if password incorrect
                TempData["Error"] = "Wrong credentials. Please try again.";
                return View(loginVM);
            }
            //if user with this email was not found
            TempData["Error"] = "Wrong credentials. Please try again.";
            return View(loginVM);
        }

        [HttpGet]
        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }
            //Check user`s email
            var user = await _userManager.FindByEmailAsync(registerVM.Email);
            //In case if user with this email already exist
            if (user != null)
            {
                TempData["Error"] = "User with this email already exist.";
                return View(registerVM);
            }

            var newUser = new User
            {
                Email = registerVM.Email,
                UserName = registerVM.Email
            };
            //Try to add user to database
            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);
            //In case if everything good, we add role for user
            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Course");
        }
    }
}