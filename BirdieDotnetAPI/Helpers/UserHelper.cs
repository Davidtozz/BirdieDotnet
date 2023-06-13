using Microsoft.AspNetCore.Mvc.Controllers;
using MySql.Data.MySqlClient;
using BirdieDotnetAPI.Models;
using MySql.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;


namespace BirdieDotnetAPI.Helpers
{
    public static class UserHelper
    {

        public static object GenerateJwtToken(User user, IConfiguration appConfiguration)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = new byte[32];
            key = Encoding.ASCII.GetBytes(appConfiguration["Jwt:Key"]!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                }),
                Expires = DateTime.UtcNow.AddMinutes(30), // expiration date
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var tokenString = tokenHandler.WriteToken(token);

            return new { Token = tokenString };
        }
    }
}
