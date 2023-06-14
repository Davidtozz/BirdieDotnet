using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BirdieDotnetAPI.Models;

namespace BirdieDotnetAPI.Models;

public partial class Message
{
    [Key]
    public int Id { get; set; }

    public int ConversationId { get; set; }

    public int SenderId { get; set; }

    public required string Content { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Conversation Conversation { get; set; } = null!;

    public virtual User Sender { get; set; } = null!;
}
