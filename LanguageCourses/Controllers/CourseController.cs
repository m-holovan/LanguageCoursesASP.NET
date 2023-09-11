using LanguageCourses.Data;
using LanguageCourses.Interfaces;
using LanguageCourses.Models;
using LanguageCourses.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LanguageCourses.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CourseController(ICourseRepository courseRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _courseRepository = courseRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _courseRepository.GetAll();
            return View(courses);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var course = await _courseRepository.GetCourseByIdAsync(id);
            return View(course);
        }

        public IActionResult Create()
        {
            var curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var createCourseViewModel = new CreateCourseViewModel { UserId = curUserId };
            return View(createCourseViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseViewModel courseVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(courseVM.Image);

                var course = new Course
                {
                    Title = courseVM.Title,
                    Description = courseVM.Description,
                    Image = result.Url.ToString(),
                    UserId = courseVM.UserId,
                    CourseCategory = courseVM.CourseCategory,
                    Address = new Address
                    {
                        City = courseVM.Address.City,
                        Street = courseVM.Address.Street,
                        State = courseVM.Address.State
                    }
                };
                _courseRepository.Add(course);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed!");
            }
            return View(courseVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _courseRepository.GetCourseByIdAsync(id);
            var curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            if (course == null)
            {
                return View("Error");
            }
            var courseVM = new EditCourseViewModel
            {
                Title = course.Title,
                Description = course.Description,
                AddressId = course.AddressId,
                Address = course.Address,
                CourseCategory = course.CourseCategory,
                URL = course.Image,
                UserId = curUserId
            };
            return View(courseVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditCourseViewModel courseVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit course");
                return View("Edit", courseVM);
            }

            var course = await _courseRepository.GetCourseByIdAsyncNoTracking(id);

            if (course != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(course.Image);

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(courseVM);
                }

                var photo = await _photoService.AddPhotoAsync(courseVM.Image);
                var newCourse = new Course
                {
                    Id = id,
                    Title = courseVM.Title,
                    Description = courseVM.Description,
                    AddressId = courseVM.AddressId,
                    Address = courseVM.Address,
                    CourseCategory = courseVM.CourseCategory,
                    Image = photo.Url.ToString(),
                    UserId = courseVM.UserId
                };

                _courseRepository.Update(newCourse);
                return RedirectToAction("Index");
            }
            else
            {
                return View(courseVM);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _courseRepository.GetCourseByIdAsync(id);
            if (course != null)
            {
                return View(course);
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, Course course)
        {
            course = await _courseRepository.GetCourseByIdAsync(id);

            if (course != null)
            {
                _courseRepository.Delete(course);
                return RedirectToAction("Index");
            }
            else
            {
                return View(course);
            }
        }
    }
}