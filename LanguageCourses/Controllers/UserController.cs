using LanguageCourses.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LanguageCourses.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserController(IUserRepository userRepository, IHttpContextAccessor contextAccessor)
        {
            _userRepository = userRepository;
            _contextAccessor = contextAccessor;

        }
        public async Task<IActionResult> Index()
        {
            var id = _contextAccessor.HttpContext.User.GetUserId();
            var user = await _userRepository.GetUserById(id);

            if (user == null)
            {
                return View("Error");
            }
            return View(user);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return View("Error");
            }
            return View(user);
        }
    }
}
