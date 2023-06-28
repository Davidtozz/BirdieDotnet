using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using BirdieDotnetAPI.Models;
using Microsoft.IdentityModel.Tokens;


namespace BirdieDotnetAPI.Services
{
    public class TokenService
    {

        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(User user, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            //? 256 bit key
            byte[] key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim("username", user.Username),
                    new Claim("role", role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30), //TODO dev only. lower duration in production
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                /* Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"] */
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        public void SetResponseTokens(User forUser, HttpResponse context, RefreshToken? refreshToken = null,  CookieOptions? accessTokenOptions = null, CookieOptions? refreshTokenOptions = null)
        {   
            string accessToken = GenerateJwtToken(forUser, role: "User");
            refreshToken ??= new RefreshToken() {
                JwtId = accessToken,
                ExpirationDate = DateTime.UtcNow.AddDays(7),
                CreationDate = DateTime.UtcNow,
                UserId = forUser.Id
            };

            accessTokenOptions ??= new () {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Secure = true
            };

            refreshTokenOptions ??= new () {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Path = "/api/user/refresh"
            };

            string encodedRefreshToken = SerializeRefreshToken(refreshToken);

            context.Cookies.Append("X-Access-Token", accessToken, accessTokenOptions);
            context.Cookies.Append("X-Refresh-Token", encodedRefreshToken, refreshTokenOptions);                
        }

        public string SerializeRefreshToken(RefreshToken token)
        {
            string serializedRefreshToken = JsonSerializer.Serialize(token);
            string encodedRefreshToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(serializedRefreshToken));

            return encodedRefreshToken;
        }

        public RefreshToken DeserializeRefreshToken(string encodedRefreshToken)
        {
            byte[] decodedRefreshToken = Convert.FromBase64String(encodedRefreshToken);
            string serializedRefreshToken = Encoding.UTF8.GetString(decodedRefreshToken);
            RefreshToken refreshToken = JsonSerializer.Deserialize<RefreshToken>(serializedRefreshToken)!;

            return refreshToken;
        }

    } 
}