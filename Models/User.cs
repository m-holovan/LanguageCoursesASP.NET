using System.ComponentModel.DataAnnotations;

namespace LanguageCourses.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public Address? Address { get; set; }
        public string? LvlOfLanguage { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
