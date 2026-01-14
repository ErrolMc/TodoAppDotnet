using System;
using System.Threading.Tasks;
using System.Web;
using TodoAppShared;

namespace TodoAppFrontend.Services.Concrete
{
    public class AuthService : IAuthService
    {
        private UserDTO _currentUser;

        public async Task<bool> LoginAsync(string username, string password)
        {
            // TODO: Replace with actual API call to backend authentication endpoint
            await Task.Delay(100); // Simulate async operation
            var loginResult = new LoginResult()
            {
                Success = true, 
                User = new UserDTO() { UserID = username, UserName = username },
                Message = "Success" 
            };

            // For now, this is a placeholder implementation
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return true;
            }

            _currentUser = loginResult.User;

            // simulate true
            return true;
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
