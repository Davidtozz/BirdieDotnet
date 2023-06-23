using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
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

        public string GenerateJwtToken(string username, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            //? 256 bit key
            byte[] key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30), // expiration date
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }

            return Convert.ToBase64String(randomNumber);
        }
    
        public void SetResponseTokens(string username, HttpResponse response)
        {
            string accessToken = GenerateJwtToken(username, role: "User");
            string refreshToken = GenerateRefreshToken();

            response.Cookies.Append("X-Access-Token", accessToken, new CookieOptions() {HttpOnly = true, SameSite = SameSiteMode.Strict, Secure = true});
            response.Cookies.Append("X-Refresh-Token", refreshToken, new CookieOptions() {HttpOnly = true, SameSite = SameSiteMode.Strict, Path = "/api/user/refresh"});                

        }
            
    
    } 
}