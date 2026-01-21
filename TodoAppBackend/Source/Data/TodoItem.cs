using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TodoAppShared;

namespace TodoAppBackend.Data
{
    [Serializable]
    [Table("TodoItems")]
    public class TodoItem
    {
        [Key]
        [MaxLength(50)]
        [JsonProperty("id")]
        public string Id { get; set; }

        [Required]
        [MaxLength(100)]
        [JsonProperty("userid")]
        public string UserID { get; set; }

        [Required]
        [MaxLength(100)]
        [JsonProperty("title")]
        public string Title { get; set; }

        [Required]
        [JsonProperty("createdate")]
        public DateTime CreateDate { get; set; }

        [Required]
        [JsonProperty("iscompleted")]
        public bool IsCompleted { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("completeddate")]
        public DateTime? CompletedDate { get; set; }

        [JsonProperty("lastediteddate")]
        public DateTime? LastEditedDate { get; set; }

        public TodoItemDTO ToDTO()
        {
            return new TodoItemDTO
            {
                Id = this.Id,
                UserID = this.UserID,
                Title = this.Title,
                IsCompleted = this.IsCompleted,
                Description = this.Description,
                CreateDate = this.CreateDate,
                CompletedDate = this.CompletedDate,
                LastEditedDate = this.LastEditedDate
            };
        }
    }
}