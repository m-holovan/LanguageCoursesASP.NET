using LanguageCourses.Data;
using LanguageCourses.Interfaces;
using LanguageCourses.Models;
using Microsoft.EntityFrameworkCore;

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
            _context.Add(teacher);
            return Save();
        }

        public bool Delete(Teacher teacher)
        {
            _context.Remove(teacher);
            return Save();
        }

        public async Task<IEnumerable<Teacher>> GetAllTeachers()
        {
            return await _context.Teachers.ToListAsync();
        }

        public async Task<Teacher> GetTeacherById(int id)
        {
            return await _context.Teachers.Include(a => a.Address).SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Teacher> GetTeacherByIdNoTracking(int id)
        {
            return await _context.Teachers.Include(a => a.Address).AsNoTracking().SingleOrDefaultAsync(t => t.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Teacher teacher)
        {
            _context.Update(teacher);
            return Save();
        }
    }
}