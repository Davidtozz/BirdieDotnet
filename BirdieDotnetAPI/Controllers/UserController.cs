using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using BirdieDotnetAPI.Models;
using System.IdentityModel.Tokens;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Runtime.InteropServices;
using BirdieDotnetAPI.Helpers;

namespace BirdieDotnetAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public sealed class UserController : ControllerBase
    {

        private readonly MySqlConnection _connection;
        private readonly IConfiguration _configuration;

        public UserController(MySqlConnection connection, IConfiguration configuration) 
        {
            _connection = connection;
            _configuration = configuration;
        }

        //TODO Configure Authorize attributes 

        //[Authorize]
        [HttpGet] //! /api/user
        public IActionResult GetAllUsers()
        {
            List<User> UserList = new();
            using MySqlConnection connection = _connection;
            using MySqlCommand command = connection.CreateCommand();
            try 
            {
                connection.Open();
            }
            catch (MySqlException) 
            {
                return StatusCode(500, "Internal server error");
            }
            
            command.CommandText = "SELECT * FROM users";
            using MySqlDataReader reader = command.ExecuteReader();

            // Loop through query results
            while (reader.Read())
            {
                var user = new User{
                    Id = reader.GetInt32("id"),
                    Name = reader.GetString("name"),
                    Psw = reader.GetString("password")
                };
                UserList.Add(user);
            }
            
            connection.Close();
            var SerializedUserList = JsonConvert.SerializeObject(UserList);

            return Ok(SerializedUserList);
        }

        //[Authorize]
        [HttpGet("{id}")] //! /api/user/{id}
        public IActionResult GetUserById(int id)
        {
            User? user = null;
            using MySqlConnection connection = _connection;
            using MySqlCommand command = connection.CreateCommand();

            try
            {
                connection.Open();
            }
            catch (MySqlException)
            {
                return StatusCode(500, "Internal server error");
            }

            #region SearchUserById
            command.CommandText = "SELECT * FROM user WHERE id = @id";
            command.Parameters.AddWithValue("@id", id);

            // Execute the query and retrieve the results
            using MySqlDataReader results = command.ExecuteReader();
            
            // Check if user exists
            if (results.Read())
            {
                user = new User
                {
                    Id = results.GetInt32("id"),
                    Name = results.GetString("name"),
                    Psw = results.GetString("password")
                };
                connection.Close();
            }
            #endregion

            var JsonResult = (user != null) ? JsonConvert.SerializeObject(user) : "";
            return Ok(JsonResult);
        }

        [AllowAnonymous]
        [HttpPost("register")] //! /api/user/new
        public IActionResult RegisterUser([FromBody] User user, IConfiguration configuration)
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
        }

    }
}