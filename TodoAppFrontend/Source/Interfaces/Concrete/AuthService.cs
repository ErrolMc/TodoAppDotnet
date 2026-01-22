using System.Threading.Tasks;
using TodoAppFrontend.Source;
using TodoAppShared;
using TodoAppFrontend.Http;

namespace TodoAppFrontend.Services.Concrete
{
    public class AuthService : HttpService, IAuthService
    {
        private UserDTO _currentUser;

        public async Task<Result> LoginAsync(string username, string password)
        {
            var request = new LoginRequest()
            {
                Username = username,
                Password = password
            };

            ApiResponse<LoginResult> response = await HttpHelper.PostAsync<LoginRequest, LoginResult>(HttpClient, "api/auth/login", request);

            if (response.IsSuccessful)
            {
                LoginResult loginResult = response.Data;
                
                // Deserialize the response
                if (loginResult != null && loginResult.Success)
                {
                    _currentUser = loginResult.User;
                    return Result.Success();
                }
            }

            return Result.Failure("Login failed");
        }

        public async Task<Result> LogoutAsync()
        {
            await Task.Delay(100);
            _currentUser = null;
            return Result.Success();
        }

        public async Task<Result> RegisterAsync(string username, string password)
        {
            var request = new LoginRequest()
            {
                Username = username,
                Password = password
            };

            ApiResponse<string> response = await HttpHelper.PostAsync<LoginRequest, string>(HttpClient, "api/auth/register", request);

            return response;
        }

        public UserDTO GetCurrentUser() => _currentUser;
        
        public bool IsLoggedIn() => _currentUser != null;
    }
}
