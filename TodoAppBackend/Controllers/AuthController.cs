using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using TodoAppBackend.Repositories;
using TodoAppShared;
using User = TodoAppBackend.Data.User;

namespace TodoAppBackend.Controllers
{
    public class AuthController : ApiController
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("api/auth/login")]
        public async Task<IHttpActionResult> Login(LoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Username and password are required.");
            }

            User user = await _userRepository.GetUserByUsernameAsync(request.Username);
            
            if (user == null)
            {
                return Unauthorized();
            }

            if (!ValidatePassword(request.Password, user.PasswordHash))
            {
                return Unauthorized();
            }

            var result = new LoginResult
            {
                Success = true,
                User = user.ToDTO(),
                Message = "Login successful"
            };

            return Ok(result);
        }

        [HttpPost]
        [Route("api/auth/register")]
        public async Task<IHttpActionResult> Register(LoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Incorrect username or password");
            }

            User user = await _userRepository.GetUserByUsernameAsync(request.Username);

            if (user != null)
            {
                return BadRequest("User already exists.");
            }

            var newUser = new Data.User
            {
                UserID = Guid.NewGuid().ToString(),
                Username = request.Username,
                PasswordHash = HashPassword(request.Password)
            };

            bool result = await _userRepository.CreateUserAsync(newUser);

            if (!result)
            {
                return InternalServerError();
            }

            return Ok("User created successfully!");
        }

        private static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private static bool ValidatePassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
