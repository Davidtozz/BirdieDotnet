using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
//using BirdieDotnetAPI.Models; //? will be removed soon
using BirdieDotnetAPI.ModelsEF;
using System.IdentityModel.Tokens;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Runtime.InteropServices;
using BirdieDotnetAPI.Helpers;
using System.Data;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using BirdieDotnetAPI.Data;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace BirdieDotnetAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public sealed class UserController : ControllerBase
    {

        private readonly TestContext _context;
        private readonly IConfiguration _configuration; //? Used signing JWT 
        

        public UserController(TestContext context, IConfiguration configuration) 
        {
            _context = context;
            _configuration = configuration;
        }

        //TODO Configure Authorize attributes 


        [HttpGet] //? /api/user
        public IActionResult GetAllUsers()
        {
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

        /*
        [AllowAnonymous]
        [HttpPost("register")] //! /api/user/new
        public IActionResult RegisterUser([FromBody] User user)
        {
            //! debug
            //Console.WriteLine($"user:\n{user.Name}\n{user.Psw}");      
            using MySqlConnection Connection = _connection;
            
            try
            {
                Connection.Open();
            }
            catch (MySqlException)
            {
                return StatusCode(500, "Internal server error");
            }

            #region CheckIfUserExists

            using MySqlCommand Command = Connection.CreateCommand();
            Command.CommandText = "SELECT * FROM users WHERE users.username = @name;";
            Command.Parameters.AddWithValue("@name", user.Name);
            

            if (Command.ExecuteScalar() != null) return StatusCode(401,"User already exists");

            #endregion

            #region CreateNewUser
            
            Command.CommandText = "INSERT INTO users (users.username, users.password) VALUES (@name, @psw);";
            Command.Parameters.AddWithValue("@psw", user.Psw);
            int RowsAffected = Command.ExecuteNonQuery();
            Connection.Close();

            return (RowsAffected > 0) ? Ok(UserHelper.GenerateJwtToken(user, _configuration)) : StatusCode(500, "Error while adding user");           
            #endregion
        }

        
        [AllowAnonymous] 
        [HttpPost("login")] //! /api/user/login
        public IActionResult LoginUser([FromBody] User user) 
        {
            #region CredentialValidation

            using MySqlConnection Connection = _connection;
            
            try
            {
                Connection.Open();
            }
            catch (MySqlException)
            {
                return StatusCode(500, "Internal server error");
            }

            using MySqlCommand Command = Connection.CreateCommand();

            Command.CommandText = "SELECT users.username, users.password FROM users WHERE users.username = @name AND users.password = @psw;";
            Command.Parameters.AddWithValue("@name", user.Name);
            Command.Parameters.AddWithValue("@psw", user.Psw);

            #endregion

            if (Command.ExecuteScalar() != null)
            {
                //! DEBUG
                //Console.WriteLine("Closing connection...");
                Connection.Close();

                return Ok(UserHelper.GenerateJwtToken(user, _configuration));

            }
            else
            {
                Connection.Close();
                return StatusCode(StatusCodes.Status401Unauthorized, "Invalid credentials");
            }
        }*/

    }
}