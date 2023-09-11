using LanguageCourses.Models;

namespace LanguageCourses.Interfaces
{
    public interface IDashboardRepository
    {
        public Task<List<Course>> GetAllUserCourses();
        public Task<List<Teacher>> GetAllUserTeacher();
    }
}
