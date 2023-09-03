using Microsoft.AspNetCore.Mvc;

namespace LanguageCourses.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
