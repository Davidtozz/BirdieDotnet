using System;
using System.Collections.Generic;

namespace BirdieDotnetAPI.ModelsEF;

public partial class Conversation
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();
}
