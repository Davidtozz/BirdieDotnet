﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BirdieDotnetAPI.Models;

//TODO Migrate to Identity User 

public class User  /* IdentityUser<int> */
{
    [Key]
    public int Id { get; set; }

    public required string Username { get; set; }

    public required string Password { get; set; }

    public DateTime CreatedAt { get; set; }

    public required string Email { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();
}