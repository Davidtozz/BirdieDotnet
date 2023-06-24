using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BirdieDotnetAPI.Models
{
    public class RefreshToken
    {
        [Key]
        [JsonIgnore]
        public int Id {get; set;}
        
        [Required]
        public string JwtId {get; set;}
        
        public DateTime ExpirationDate {get; set;}
        
        public DateTime CreationDate {get; set;}

        [JsonIgnore]
        public int UserId {get; set;}
        
        [JsonIgnore]
        public virtual User User {get; set;} = null!;
    }
}