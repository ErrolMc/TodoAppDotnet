using System;
using Newtonsoft.Json;

namespace TodoAppShared
{
    [Serializable]
    public class LoginResult
    {
        [JsonProperty("success")] public bool Success { get; set; }
        [JsonProperty("user")] public UserDTO User { get; set; }
        [JsonProperty("message")] public string Message { get; set; }
    }
}
