using LanguageCourses.Data;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using LanguageCourses.Models;
using LanguageCourses.Data.Enum;
using LanguageCourses.Repository;

namespace LanguageCourses.Tests.Repository
{
    public class CourseRepositoryTests
    {
        private async Task<ApplicationDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();

            if (await databaseContext.Courses.CountAsync() < 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Courses.Add(
                        new Course()
                        {
                            Title = "Language Course",
                            Image = "",
                            Description = "Description for course",
                            CourseCategory = CourseCategory.A1,
                            Address = new Address()
                            {
                                State = "Germany",
                                City = "Dresden",
                                Street = "Hauptstrasse"
                            }
                        });
                }
                await databaseContext.SaveChangesAsync();
            }
            return databaseContext;
        }

        [Fact]
        public async void CourseRepository_Add()
        {
            //Arrange
            var course = new Course
            {
                Title = "Language Course",
                Image = "",
                Description = "Description for course",
                CourseCategory = CourseCategory.A1,
                Address = new Address()
                {
                    State = "Germany",
                    City = "Dresden",
                    Street = "Hauptstrasse"
                }
            };

            var dbContext = await GetDbContext();
            var courseRepository = new CourseRepository(dbContext);

            //Act
            var result = courseRepository.Add(course);

            //Assert
            result.Should().BeTrue();
        }
    }
}
