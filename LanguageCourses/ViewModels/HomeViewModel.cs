using LanguageCourses.Models;

namespace LanguageCourses.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Course>? Courses { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
    }
}
