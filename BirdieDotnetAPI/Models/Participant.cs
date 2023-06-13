using System;
using System.Collections.Generic;

namespace BirdieDotnetAPI.Models;

public partial class Participant
{
    public int Id { get; set; }

    public int ConversationId { get; set; }

    public int UserId { get; set; }

    public virtual Conversation Conversation { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
