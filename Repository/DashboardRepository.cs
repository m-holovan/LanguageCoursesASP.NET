using LanguageCourses.Data;
using LanguageCourses.Interfaces;
using LanguageCourses.Models;

namespace LanguageCourses.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

        }
        public async Task<List<Course>> GetAllUserCourses()
        {
            var curUser = _httpContextAccessor.HttpContext?.User;
            var userCourses = _context.Courses.Where(c => c.User.Id == curUser.ToString());

            return userCourses.ToList();
        }

        public async Task<List<Teacher>> GetAllUserTeacher()
        {
            var curUser = _httpContextAccessor.HttpContext?.User;
            var userTeachers = _context.Teachers.Where(t => t.User.Id == curUser.ToString());

            return userTeachers.ToList();
        }
    }
}
