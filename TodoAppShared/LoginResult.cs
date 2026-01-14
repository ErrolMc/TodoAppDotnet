using System;

namespace TodoAppShared
{
    [Serializable]
    public class LoginResult
    {
        public bool Success { get; set; }
        public UserDTO User { get; set; }
        public string Message { get; set; }
    }
}
