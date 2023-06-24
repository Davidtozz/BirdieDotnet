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
using BirdieDotnetAPI.Services;

namespace BirdieDotnetAPI.Controllers
{
    [ApiController]
    [Authorize] 
    [Route("api/[controller]")]
    public sealed class UserController : ControllerBase
    {

        //? ef db instance
        private readonly TestContext _context; 
        private readonly TokenService _tokenService;
        

        public UserController(TestContext context, TokenService tokenService) 
        {
            _context = context;
            _tokenService = tokenService;
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
            
            _tokenService.SetResponseTokens(username: user.Username, response: Response);

            return Ok();
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

            string jwtToken = _tokenService.GenerateJwtToken(user.Username, role: "User");
            //string refreshToken = _tokenService.GenerateRefreshToken();

            _tokenService.SetResponseTokens(username: user.Username, Response);

            return Ok();
        }

        
        [Authorize(Policy = "RefreshExpiredToken")] 
        [HttpPost("refresh")]
        public IActionResult RefreshToken() //! Only the refresh token should be sent here 
        {
            /* if(!Request.Cookies.ContainsKey("X-Refresh-Token"))
            {
                return Unauthorized("No refresh token found");
            }


            _context.Tokens.Add(_tokenService.GenerateRefreshToken());
            
         
         
            _tokenService.GenerateRefreshToken(); */
         
         
            return Ok();
        }
    }
}