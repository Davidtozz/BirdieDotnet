using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using BirdieDotnetAPI.Models;
using System.Xml.Linq;

namespace BirdieDotnetAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public sealed class UserController : ControllerBase
    {

        private readonly MySqlConnection _connection;

        public UserController(MySqlConnection connection) 
        {
            _connection = connection;
            
        }

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

        [HttpPost("new")] //! /api/user/new
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
            Command.Parameters.AddWithValue("@psw", user.Psw);

            if (Command.ExecuteScalar() != null) return Ok("User already exists");

            #endregion

            #region CreateNewUser
            
            Command.CommandText = "INSERT INTO users (users.username, users.password) VALUES (@name, @psw);";
            int RowsAffected = Command.ExecuteNonQuery();
            Connection.Close();

            return (RowsAffected > 0) ? Ok("User added successfully") : StatusCode(500, "Error while adding user");           
            #endregion
        }

        [HttpPost("login")] //! /api/user/login
        public IActionResult LoginUser([FromBody] User user) 
        {
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
            if (Command.ExecuteScalar() != null)
            {
                Connection.Close();
                return Ok($"User {user.Name} logged in");
            }
            else
            {
                Connection.Close();
                return StatusCode(StatusCodes.Status401Unauthorized, "Invalid credentials");
            }
        }
    }
}