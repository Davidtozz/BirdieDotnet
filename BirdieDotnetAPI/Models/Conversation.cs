namespace BirdieDotnetAPI.Models
{
    public class Conversation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> ParticipantsIds { get; set; }
        public DateTime CreatedAt { get; set; }    
    }
}
