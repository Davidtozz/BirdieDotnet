using Microsoft.AspNetCore.Mvc;



namespace BirdieDotnetAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {

            Console.WriteLine("Received GET at /user");
            return Ok();
        }

        [HttpPost]
        public IActionResult Post()
        {
        
            string RequestBody = HttpContext.Request.Body.ToString();

            Console.WriteLine(RequestBody);

            return Ok();
        }

    }
}
