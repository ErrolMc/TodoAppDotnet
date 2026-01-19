using System;
using Newtonsoft.Json;

namespace TodoAppShared
{
    [Serializable]
    public class LoginRequest
    {
        [JsonProperty("username")] public string Username { get; set; }
        [JsonProperty("password")] public string Password { get; set; }
    }
}
