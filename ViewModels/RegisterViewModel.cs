using System.ComponentModel.DataAnnotations;

namespace LanguageCourses.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password are not the same.")]
        public string ConfirmPassword { get; set; }
    }
}
