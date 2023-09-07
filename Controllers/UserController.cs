using LanguageCourses.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LanguageCourses.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsers();
            return  View(users);
        }
    }
}
