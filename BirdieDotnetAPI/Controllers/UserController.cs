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
using BirdieDotnetAPI.Helpers;
using Microsoft.EntityFrameworkCore;
using BirdieDotnetAPI.Data;



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
        public IActionResult GetAllUsers([FromHeader] Dictionary<string, string> headers)
        {
            Console.WriteLine(JsonConvert.SerializeObject(headers));
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
            
            return Ok(UserHelper.GenerateJwtToken(user, _configuration));
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

            

            return Ok(UserHelper.GenerateJwtToken(user, _configuration));
        }
    }
}