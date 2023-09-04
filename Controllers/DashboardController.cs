using LanguageCourses.Interfaces;
using LanguageCourses.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LanguageCourses.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }
        public async Task<IActionResult> Index()
        {
            var userCourses = await _dashboardRepository.GetAllUserCourses();
            var userTeachers = await _dashboardRepository.GetAllUserTeacher();

            var dashboardViewModel = new DashboardViewModel
            {
                Courses = userCourses,
                Techers = userTeachers
            };

            return View(dashboardViewModel);
        }
    }
}
