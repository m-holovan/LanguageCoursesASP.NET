using LanguageCourses.Data.Enum;
using LanguageCourses.Models;

namespace LanguageCourses.ViewModels
{
    public class CreateCourseViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
        public IFormFile Image { get; set; }
        public CourseCategory CourseCategory { get; set; }
    }
}
