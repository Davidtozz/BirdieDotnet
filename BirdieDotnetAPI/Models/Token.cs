using System.ComponentModel.DataAnnotations;

namespace BirdieDotnetAPI.Models
{
    public class Token
    {
        [Key]
        public int Id {get; set;}
        
        [Required]
        public string Refresh {get; set;}

        public DateTime Expires {get; set;}

        public bool IsExpired => DateTime.UtcNow >= Expires;

        public DateTime CreatedAt {get; set;}

        public DateTime? RevokedAt {get; set;}
   
        public virtual User User {get; set;} = null!;

        public int UserId {get; set;}
   
    }
}