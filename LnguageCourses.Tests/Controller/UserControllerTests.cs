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
    public class UserControllerTests
    {
        private IUserRepository _userRepository;
        private IHttpContextAccessor _contextAccessor;
        private UserController _userController;

        public UserControllerTests()
        {
            _userRepository = A.Fake<IUserRepository>();
            _contextAccessor = A.Fake<IHttpContextAccessor>();

            _userController = new UserController(_userRepository, _contextAccessor);
        }

        [Fact]
        public void UserController_Index_ReturnSuccess()
        {
            var id = _contextAccessor.HttpContext.User.GetUserId();
            var user = A.Fake<User>();

            A.CallTo(() => _userRepository.GetUserById(id)).Returns(user);

            var result = _userController.Index();

            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void UserController_Detail_ReturnSuccess()
        {
            var id = _contextAccessor.HttpContext.User.GetUserId();
            var user = A.Fake<User>();

            A.CallTo(() => _userRepository.GetUserById(id)).Returns(user);

            var result = _userController.Detail(id);

            result.Should().BeOfType<Task<IActionResult>>();
        }
    }
}
