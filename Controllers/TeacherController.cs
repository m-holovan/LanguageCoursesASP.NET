using LanguageCourses.Data;
using LanguageCourses.Interfaces;
using LanguageCourses.Models;
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
        public async Task<IActionResult> Index()
        {
            var teachers = await _teacherRepository.GetAllTeachers();
            return View(teachers);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var teacher = await _teacherRepository.GetTeacherById(id);
            if (teacher == null)
            {
                return View("Error");
            }
            return View(teacher);
        }
    }
}
