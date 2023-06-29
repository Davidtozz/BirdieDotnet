using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

#nullable disable

namespace BirdieDotnetAPI.Models
{
    public record RefreshToken
    {
        
        [Key, JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id {get; set;}
        
        [Required]
        [Column(TypeName = "varchar(255)")]
        public string JwtId {get; set;}
        
        [JsonIgnore]
        [Column(TypeName = "datetime")]
        public DateTime ExpirationDate {get; set;}
        
        [Column(TypeName = "datetime")]
        public DateTime CreationDate {get; set;}

        [JsonIgnore]
        [Column(TypeName = "int(11)")]
        public int UserId {get; set;}
        
        [JsonIgnore]
        public virtual User User {get; set;} = null!;

    }
}