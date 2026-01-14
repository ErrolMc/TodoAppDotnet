using System.Threading.Tasks;
using TodoAppShared;

namespace TodoAppFrontend.Services
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(string username, string password);
        Task<bool> LogoutAsync();
        Task<bool> RegisterAsync(string username, string password);
        bool IsLoggedIn();
        UserDTO GetCurrentUser();
    }
}
