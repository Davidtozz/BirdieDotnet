using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

#nullable disable

namespace BirdieDotnetAPI.Models
{
    public record RefreshToken
    {
        
        [Key, JsonIgnore]
        public int Id {get; set;}
        
        [Required]
        public string JwtId {get; set;}
        
        [JsonIgnore]
        public DateTime ExpirationDate {get; set;}
        
        public DateTime CreationDate {get; set;}

        [JsonIgnore]
        public int UserId {get; set;}
        
        [JsonIgnore]
        public virtual User User {get; set;} = null!;

    }
}