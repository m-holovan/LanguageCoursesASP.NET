using LanguageCourses.Models;
using Microsoft.EntityFrameworkCore;

namespace LanguageCourses.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
    }
}