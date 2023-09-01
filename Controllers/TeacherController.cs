using LanguageCourses.Data;
using LanguageCourses.Interfaces;
using LanguageCourses.Models;
using LanguageCourses.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LanguageCourses.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IPhotoService _photoService;
        public TeacherController(ITeacherRepository teacherRepository, IPhotoService photoService)
        {
            _teacherRepository = teacherRepository;
            _photoService = photoService;
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTeacherViewModel teacherVM)
        {
            if (ModelState.IsValid)
            {
                var photo = await _photoService.AddPhotoAsync(teacherVM.Image);
                var teacher = new Teacher
                {
                    Name = teacherVM.Name,
                    Surname = teacherVM.Surname,
                    Age = teacherVM.Age,
                    DetailInformation = teacherVM.DetailInformation,
                    CourseCategory = teacherVM.CourseCategory,
                    Image = photo.Url.ToString(),
                    Address = new Address
                    {
                        City = teacherVM.Address.City,
                        State = teacherVM.Address.State,
                        Street = teacherVM.Address.Street
                    }
                };
                _teacherRepository.Add(teacher);
                return RedirectToAction("Index");
            }

            else
            {
                ModelState.AddModelError("", "Photo upload failed!");
            }
            return View(teacherVM);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var teacher = await _teacherRepository.GetTeacherById(id);

            if (teacher == null)
            {
                return View("Error");
            }
            return View(teacher);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, Teacher teacher)
        {
            teacher = await _teacherRepository.GetTeacherById(id);
            if (teacher == null)
            {
                return View("Error");
            }
            _teacherRepository.Delete(teacher);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var teacher = await _teacherRepository.GetTeacherById(id);

            if (teacher == null)
            {
                return View("Error");
            }
            var teacherVM = new EditTeacherViewModel
            {
                Name = teacher.Name,
                Surname = teacher.Surname,
                Age = teacher.Age,
                AddressId = teacher.AddressId,
                Address = teacher.Address,
                URL = teacher.Image,
                CourseCategory = teacher.CourseCategory,
                DetailInformation = teacher.DetailInformation
            };
            return View(teacherVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditTeacherViewModel teacherVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit teacher");
                return View("Edit", teacherVM);
            }

            var teacher = await _teacherRepository.GetTeacherByIdNoTracking(id);
            if (teacher != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(teacher.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(teacherVM);
                }

                var photo = await _photoService.AddPhotoAsync(teacherVM.Image);
                var newTeacher = new Teacher
                {
                    Id = id,
                    Name = teacherVM.Name,
                    Surname = teacherVM.Surname,
                    DetailInformation = teacherVM.DetailInformation,
                    Image = photo.Url.ToString(),
                    CourseCategory = teacherVM.CourseCategory,
                    AddressId = teacherVM.AddressId,
                    Address = teacherVM.Address
                };
                _teacherRepository.Update(newTeacher);
                return RedirectToAction("Index");
            }
            else
            {
                return View(teacherVM);
            }
        }
    }
}