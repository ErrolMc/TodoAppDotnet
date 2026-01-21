using Newtonsoft.Json;
using System;

namespace TodoAppShared
{
    [Serializable]
    public class TodoItemDTO
    {
        [JsonProperty("id")] public string Id { get; set; }
        [JsonProperty("userid")] public string UserID { get; set; }
        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("iscompleted")] public bool IsCompleted { get; set; }
        [JsonProperty("description")] public string Description { get; set; }
        [JsonProperty("createdate")] public DateTime CreateDate { get; set; }
        [JsonProperty("completeddate")] public DateTime? CompletedDate { get; set; }
        [JsonProperty("lastediteddate")] public DateTime? LastEditedDate { get; set; }
    }
}
