using LanguageCourses.Models;

namespace LanguageCourses.Interfaces
{
    public interface ITeacherRepository
    {
        public Task<IEnumerable<Teacher>> GetAllTeachers();
        public Task<Teacher> GetTeacherById(int id);
        public Task<Teacher> GetTeacherByIdNoTracking(int id);
        public bool Add(Teacher teacher);
        public bool Delete(Teacher teacher);
        public bool Update(Teacher teacher);
        public bool Save();
    }
}
