using LanguageCourses.Data;
using LanguageCourses.Interfaces;
using LanguageCourses.Models;

namespace LanguageCourses.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ApplicationDbContext _context;
        public TeacherRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Teacher teacher)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Teacher teacher)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Teacher>> GetAllTeachers(Teacher teacher)
        {
            throw new NotImplementedException();
        }

        public Task<Teacher> GetTeacherById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool Update(Teacher teacher)
        {
            throw new NotImplementedException();
        }
    }
}
