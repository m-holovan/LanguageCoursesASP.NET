using LanguageCourses.Data.Enum;
using LanguageCourses.Models;

namespace LanguageCourses.ViewModels
{
    public class CreateTeacherViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string DetailInformation { get; set; }
        public int Age { get; set; }
        public Address Address { get; set; }
        public IFormFile Image { get; set; }
        public CourseCategory CourseCategory { get; set; }
    }
}
