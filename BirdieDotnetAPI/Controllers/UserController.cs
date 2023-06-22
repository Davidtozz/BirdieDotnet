using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BirdieDotnetAPI.Models;
using System.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Runtime.InteropServices;
//TODO BirdieDotnetAPI.Helpers
using Microsoft.EntityFrameworkCore;
using BirdieDotnetAPI.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Security;

namespace BirdieDotnetAPI.Controllers
{
    [ApiController]
    [Authorize] 
    [Route("api/[controller]")]
    public sealed class UserController : ControllerBase
    {

        private readonly TestContext _context;
        private readonly IConfiguration _configuration; //? Used for signing JWT 
        

        public UserController(TestContext context, IConfiguration configuration) 
        {
            _context = context;
            _configuration = configuration;
        }

        //TODO Configure Authorize attributes 
        [HttpGet] //? /api/user
        
        public IActionResult GetAllUsers()
        {
            Console.WriteLine(HttpContext.Request.Headers.Authorization);
            //? Selects all rows in DB
            var users = _context.Users;
            
            var SerializedUserList = JsonConvert.SerializeObject(users, Formatting.Indented);
            
            return Ok(SerializedUserList);   
        }

        [HttpGet("{id}")] //? /api/user/{id}
        public async Task<IActionResult> GetUserById(int id)
        {

            var user = await _context.Users.FindAsync(id);

            if(user == null)
            {
                return NotFound();
            }

            var SerializedUser = JsonConvert.SerializeObject(user,Formatting.Indented);

            return Ok(SerializedUser);
        }

        [AllowAnonymous]
        [HttpPost("register")] //! /api/user/register
        public async Task<IActionResult> RegisterUser([FromBody] User user)
        {
            //TODO Add Exception handling

            var foundUser = await _context.Users.FirstOrDefaultAsync((u) => u.Username == user.Username);

            if(foundUser != null)
            {
                return Conflict("Can't register: user already exists"); //? HTTP 409
            }
            else
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            
            return Ok(GenerateJwtToken(user));
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] User user)
        {
            var foundUser = await _context.Users.FirstOrDefaultAsync((u) => u.Username == user.Username);

            if(foundUser == null)
            {
                return Unauthorized("Invalid username or password"); //? HTTP 401
            }

            string jwtToken = GenerateJwtToken(user);
            string refreshToken = GenerateRefreshToken();

            Response.Cookies.Append("X-Access-Token", jwtToken, new CookieOptions() {HttpOnly = true, SameSite = SameSiteMode.Strict});
            Response.Cookies.Append("X-Refresh-Token", refreshToken, new CookieOptions() {HttpOnly = true, SameSite = SameSiteMode.Strict});


            return Ok();
        }


        //TODO Move JWT logic in separate controller

        [HttpPost("/refresh")]
        public IActionResult RefreshToken([FromBody] User user) 
        {
            if(Request.Cookies["X-Refresh-Token"].IsNullOrEmpty()) {
                return Unauthorized("Refresh token not found.");
            }

            Response.Cookies.Append("X-Access-Token", GenerateJwtToken(user), new CookieOptions() {HttpOnly = true, SameSite = SameSiteMode.Strict});
            Response.Cookies.Append("X-Refresh-Token", GenerateRefreshToken(), new CookieOptions() {HttpOnly = true, SameSite = SameSiteMode.Strict});                
            return Ok();
        }

        #region UtilityMethods
       
        private string GenerateJwtToken(User user)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            
            //? 256 bit key
            byte[] key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.UtcNow.AddSeconds(30), // expiration date
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }

            return Convert.ToBase64String(randomNumber);
        }

        #endregion

    }
}