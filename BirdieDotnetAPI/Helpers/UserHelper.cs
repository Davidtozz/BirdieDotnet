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

            // Key from appsettings.json
            byte[] key = new byte[32];
            key = Encoding.ASCII.GetBytes(appConfiguration["Jwt:Key"]);

            // Create the token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim(ClaimTypes.Name, user.Name)
                    }),
                Expires = DateTime.UtcNow.AddDays(1), // expiration date
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            // Generate the token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Write the token as a string
            var tokenString = tokenHandler.WriteToken(token);

            return new { Token = tokenString };
        }


    }
}
