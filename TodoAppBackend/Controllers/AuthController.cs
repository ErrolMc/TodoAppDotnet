using System;
using System.Collections.Generic;
using System.Web.Http;
using TodoAppShared;

namespace TodoAppBackend.Controllers
{
    public class AuthController : ApiController
    {
        [HttpPost]
        [Route("api/auth/login")]
        public IHttpActionResult Login(LoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Username and password are required.");
            }

            var result = new LoginResult
            {
                Success = true,
                User = new UserDTO
                {
                    UserID = Guid.NewGuid().ToString(),
                    UserName = request.Username
                },
                Message = "Login successful"
            };

            return Ok(result);
        }
    }
}
