using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BirdieDotnetAPI.ModelsEF;

public partial class User
{
    public int Id { get; set; }

    public required string Username { get; set; }

    public required string Password { get; set; }

    public DateTime CreatedAt { get; set; }

    
    public required string Email { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();
}
