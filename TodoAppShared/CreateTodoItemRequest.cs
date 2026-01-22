using Newtonsoft.Json;
using System;

namespace TodoAppShared
{
    [Serializable]
    public class CreateTodoItemRequest
    {
        [JsonProperty("userid")] public string UserID { get; set; }
        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("description")] public string Description { get; set; }
    }
}
