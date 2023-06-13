namespace BirdieDotnetAPI.Models
{
    [Obsolete("Replaced in favor of EF Core")]
    public class Message
    {
        public int Id { get; set; }
        public int ConversationId { get; set; }
        public int SenderId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
