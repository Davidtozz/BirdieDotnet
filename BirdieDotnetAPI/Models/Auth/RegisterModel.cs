using System.ComponentModel.DataAnnotations.Schema;
using BirdieDotnetAPI.Models;

namespace BirdieDotnetAPI.Models.Auth
{   
    [NotMapped]
    public class RegisterModel : LoginModel
    {
        public required string Email { get; set; }

    }
}