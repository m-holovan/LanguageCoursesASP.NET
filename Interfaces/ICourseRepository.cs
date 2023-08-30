using LanguageCourses.Models;

namespace LanguageCourses.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAll();
        Task<Course> GetCourseByIdAsync(int id);
        Task<Course> GetCourseByIdAsyncNoTracking(int id);
        Task<IEnumerable<Course>> GetCourseByCity(string city);
        bool Add(Course course);
        bool Update(Course course);
        bool Delete(Course course);
        bool Save();
    }
}
