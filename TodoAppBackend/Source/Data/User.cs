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
    [Table("Users")]
    public class User
    {
        [Key]
        [MaxLength(50)]
        [JsonProperty("userid")] 
        public string UserID { get; set; }
        
        [Required]
        [MaxLength(100)]
        [Index(IsUnique = true)]
        [JsonProperty("username")] 
        public string Username { get; set; }
        
        [Required]
        [MaxLength(256)]
        [JsonProperty("passwordhash")] 
        public string PasswordHash { get; set; }

        public UserDTO ToDTO()
        {
            return new UserDTO
            {
                UserID = this.UserID,
                UserName = this.Username
            };
        }
    }
}