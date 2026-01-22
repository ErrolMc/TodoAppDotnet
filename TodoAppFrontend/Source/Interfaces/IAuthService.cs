using System.Threading.Tasks;
using TodoAppFrontend.Source;
using TodoAppShared;

namespace TodoAppFrontend.Services
{
    public interface IAuthService
    {
        Task<Result> LoginAsync(string username, string password);
        Task<Result> LogoutAsync();
        Task<Result> RegisterAsync(string username, string password);
        bool IsLoggedIn();
        UserDTO GetCurrentUser();
    }
}
