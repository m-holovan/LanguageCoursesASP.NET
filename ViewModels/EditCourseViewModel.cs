using LanguageCourses.Data.Enum;
using LanguageCourses.Models;

namespace LanguageCourses.ViewModels
{
    public class EditCourseViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
        public int AddressId { get; set; }
        public IFormFile Image { get; set; }
        public string? URL { get; set; }
        public CourseCategory CourseCategory { get; set; }
        public string UserId { get; set; }
    }
}
