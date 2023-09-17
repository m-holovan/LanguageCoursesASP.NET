using FakeItEasy;
using FluentAssertions;
using LanguageCourses.Controllers;
using LanguageCourses.Interfaces;
using LanguageCourses.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageCourses.Tests.Controller
{
    public class TeacherControllerTests
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IPhotoService _photoService;
        private readonly TeacherController _teacherController;

        public TeacherControllerTests()
        {
            //Dependencies
            _teacherRepository = A.Fake<ITeacherRepository>();
            _photoService = A.Fake<IPhotoService>();

            //SUT
            _teacherController = new TeacherController(_teacherRepository, _photoService);
        }

        [Fact]
        public void TeacherController_Index_ReturnsSuccess()
        {
            //Arrange
            var teachers = A.Fake<IEnumerable<Teacher>>();
            A.CallTo(() => _teacherRepository.GetAllTeachers()).Returns(teachers);

            //Act
            var result = _teacherController.Index();

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void TecherController_Detail_ReturnsSuccess()
        {
            //Arrange
            int id = 1;
            var teacher = A.Fake<Teacher>();
            A.CallTo(() => _teacherRepository.GetTeacherById(id)).Returns(teacher);

            //Act
            var result = _teacherController.Detail(id);

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }
    }
}
