using LanguageCourses.Data;
using LanguageCourses.Interfaces;
using LanguageCourses.Models;

namespace LanguageCourses.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public async Task<User> GetUserById(string id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}
