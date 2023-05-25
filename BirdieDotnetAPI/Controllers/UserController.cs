using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

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

        [HttpGet]
        public IActionResult GetAllUsers()
        {

            Console.WriteLine("Received GET at /api/user");

            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            return Ok();
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
