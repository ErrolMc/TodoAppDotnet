using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TodoAppBackend.Data;

namespace TodoAppBackend.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(string id);
        Task<User> GetUserByUsernameAsync(string username);
        Task<bool> CreateUserAsync(User user);
    }
}