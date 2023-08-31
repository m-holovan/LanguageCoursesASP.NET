using LanguageCourses.Data;
using LanguageCourses.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LanguageCourses.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ITeacherRepository _teacherRepository;
        public TeacherController(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }
        public IActionResult Index()
        {
            var teachers = _teacherRepository.GetAllTeachers();
            return View(teachers);
        }
    }
}
