using System.Threading.Tasks;
using TodoAppFrontend.Source;
using TodoAppShared;
using TodoAppFrontend.Http;

namespace TodoAppFrontend.Services.Concrete
{
    public class AuthService : HttpService, IAuthService
    {
        private UserDTO _currentUser;

        public async Task<bool> LoginAsync(string username, string password)
        {
            var request = new LoginRequest()
            {
                Username = username,
                Password = password
            };

            PostResponse<LoginResult> response = await HttpHelper.PostAsync<LoginRequest, LoginResult>(_httpClient, "api/auth/login", request);

            if (response.IsSuccessful)
            {
                LoginResult loginResult = response.Data;
                
                // Deserialize the response
                if (loginResult != null && loginResult.Success)
                {
                    _currentUser = loginResult.User;
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> LogoutAsync()
        {
            await Task.Delay(100);
            _currentUser = null;
            return true;
        }

        public async Task<bool> RegisterAsync(string username, string password)
        {
            // TODO: Replace with actual API call to backend registration endpoint
            await Task.Delay(100); // Simulate async operation

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            // Simulate successful registration - replace with actual API call
            return true;
        }

        public UserDTO GetCurrentUser() => _currentUser;
        
        public bool IsLoggedIn() => _currentUser != null;
    }
}
