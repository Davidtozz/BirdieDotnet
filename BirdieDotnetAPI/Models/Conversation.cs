using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace BirdieDotnetAPI.Models
{
    public class Conversation
    {
        
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        public List<int> ParticipantsIds { get; set; }
        public DateTime CreatedAt { get; set; }    
    }
}
