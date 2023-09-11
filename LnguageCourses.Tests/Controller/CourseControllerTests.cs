using FakeItEasy;
using FluentAssertions;
using LanguageCourses.Controllers;
using LanguageCourses.Interfaces;
using LanguageCourses.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageCourses.Tests.Controller
{
    public class CourseControllerTests
    {
        private ICourseRepository _courseRepository;
        private IPhotoService _photoService;
        private IHttpContextAccessor _httpContextAccessor;
        private CourseController _courseController;

        public CourseControllerTests()
        {
            //Dependencies
            _courseRepository = A.Fake<ICourseRepository>();
            _photoService = A.Fake<IPhotoService>();
            _httpContextAccessor = A.Fake<IHttpContextAccessor>();

            //SUT
            _courseController = new CourseController(_courseRepository, _photoService, _httpContextAccessor);
        }

        [Fact]
        public void CourseController_Index_ReturnsSuccess()
        {
            //Arrange - What do i need to bring in?
            var courses = A.Fake<IEnumerable<Course>>();
            A.CallTo(() => _courseRepository.GetAll()).Returns(courses);
            //Act
            var result = _courseController.Index();
            //Assert - Object check actions
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void CourseController_Detail_ReturnSuccess()
        {
            //Arrange
            var id = 1;
            var course = A.Fake<Course>();
            A.CallTo(() => _courseRepository.GetCourseByIdAsync(id)).Returns(course);
            //Act
            var result = _courseController.Detail(id);
            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }
    }
}
