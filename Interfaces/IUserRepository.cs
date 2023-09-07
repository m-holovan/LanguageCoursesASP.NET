using LanguageCourses.Models;

namespace LanguageCourses.Interfaces
{
    public interface IUserRepository
    {
        public Task<List<User>> GetAllUsers();
        public Task<User> GetUserById(string id);
    }
}
