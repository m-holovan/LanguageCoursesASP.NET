using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanguageCourses.Models
{
    public class User : IdentityUser
    {
        [Key]
        public int Id { get; set; }
        public Address? Address { get; set; }

        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public string? LvlOfLanguage { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
