using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BirdieDotnetAPI.Services;
using Microsoft.AspNetCore.Identity;
using NuGet.Common;

namespace BirdieDotnetAPI.Models;

//TODO Migrate to Identity User 

#pragma warning disable 8618

public class User  /* IdentityUser<int> */
{

    public int Id { get; set; }

    [Required]
    public string Username { get; set; }

    public string Password { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Email { get; set; }
    
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();


}