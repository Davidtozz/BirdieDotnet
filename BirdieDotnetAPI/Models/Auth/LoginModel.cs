using System.ComponentModel.DataAnnotations.Schema;
using BirdieDotnetAPI.Models;

namespace BirdieDotnetAPI.Models.Auth
{   
    [NotMapped]
    public class LoginModel 
    {

        public required string Username { get; set; }

        public required string Password { get; set; }

    }
}