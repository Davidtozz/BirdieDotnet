using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using BirdieDotnetAPI.Models;

namespace BirdieDotnetAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly MySqlConnection _connection;

        public UserController(MySqlConnection connection) 
        {
            _connection = connection;
            
        }

        [HttpGet] //! api/user/  
        public IActionResult GetAllUsers()
        {

            Console.WriteLine("Received GET at /api/user");
            var users = new List<object>();
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM user";
                _connection.Open();

                // Execute the query and retrieve the results
                using (var reader = command.ExecuteReader())
                {
                    // Process the query results as needed
                    while (reader.Read())
                    {
                        // Access the values from the query results
                        var user = new
                        {
                            Id = reader.GetInt32("id"),
                            Name = reader.GetString("name"),
                            Psw = reader.GetString("password")
                        };

                        users.Add(user);
                    }
                }

                _connection.Close();
            }

            var json = JsonConvert.SerializeObject(users);


            return Ok(json);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            Console.WriteLine($"Received GET at /api/user/{id}");

            User? user = null;

            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM user WHERE id = @id";
                command.Parameters.AddWithValue("@id", id);
                
                _connection.Open();

                // Execute the query and retrieve the results
                using (var reader = command.ExecuteReader())
                {
                    // Process the query results as needed
                    if (reader.Read())
                    { 
                        user = new User{
                        Id = reader.GetInt32("id"),
                        Name = reader.GetString("name"),
                        Psw = reader.GetString("password")
                        };
                    }
                }

                _connection.Close();
            }

            var json = (user != null) ? JsonConvert.SerializeObject(user) : "";

            return Ok(json);
        }



        [HttpPost("new")]
        public IActionResult CreateNewUser()
        {
        
            string RequestBody = HttpContext.Request.Body.ToString();

            Console.WriteLine(RequestBody);

            return Ok();
        }

        [HttpPost("login")]
        public IActionResult LoginUser() 
        { // implement later 
            return Ok(); 
        }

    }
}
