using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TodoAppBackend.Data;

namespace TodoAppBackend.Repositories
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed = false;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return null;

            return await _context.Users
                .FirstOrDefaultAsync(u => u.UserID == id);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
                return null;

            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            if (user == null || string.IsNullOrEmpty(user.UserID))
                return false;

            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context?.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
