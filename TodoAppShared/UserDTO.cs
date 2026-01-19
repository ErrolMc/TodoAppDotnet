using System;
using Newtonsoft.Json;

namespace TodoAppShared
{
    [Serializable]
    public class UserDTO
    {
        [JsonProperty("userid")] public string UserID { get; set; }
        [JsonProperty("username")] public string UserName { get; set; }
    }
}
