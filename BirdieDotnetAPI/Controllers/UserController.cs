    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using BirdieDotnetAPI.Models;
    using BirdieDotnetAPI.Models.Auth;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.EntityFrameworkCore;
    using BirdieDotnetAPI.Data;
    using BirdieDotnetAPI.Services;
    using System.Diagnostics;
using Org.BouncyCastle.Asn1.X509.Qualified;
using System.Text;
using System.ComponentModel;

namespace BirdieDotnetAPI.Controllers
    {
        [ApiController]
        [Authorize] 
        [Route("api/[controller]")]
        public sealed class UserController : ControllerBase
        {

            //? ef db instance
            private readonly TestContext _dbcontext; 
            private readonly TokenService _tokenService;
            

            public UserController(TestContext dbcontext, TokenService tokenService) 
            {
                _dbcontext = dbcontext;
                _tokenService = tokenService;
            }

            //TODO Configure Authorize attributes 
            [HttpGet] //? /api/user
            
            public IActionResult GetAllUsers()
            {
                Console.WriteLine(HttpContext.Request.Headers.Authorization);
                //? Selects all rows in DB
                var users = _dbcontext.Users;
                
                var SerializedUserList = JsonConvert.SerializeObject(users, Formatting.Indented);
                
                return Ok(SerializedUserList);   
            }

            [HttpGet("{id}")] //? /api/user/{id}
            public async Task<IActionResult> GetUserById(int id)
            {

                var user = await _dbcontext.Users.FindAsync(id);

                if(user == null)
                {
                    return NotFound();
                }

                var SerializedUser = JsonConvert.SerializeObject(user,Formatting.Indented);

                return Ok(SerializedUser);
            }

            [AllowAnonymous]
            [HttpPost("register")] //! /api/user/register
            public async Task<IActionResult> RegisterUser([FromBody] RegisterModel registeringUser)
            {
                //TODO Add Exception handling

                #region IsUserAlreadyRegistered

                var foundUser = await _dbcontext.Users.FirstOrDefaultAsync((u) => 
                u.Username == registeringUser.Username &&
                u.Username == registeringUser.Email
                );

                if(foundUser != null)
                {
                    return Conflict("Can't register: user already exists"); //? HTTP 409
                }

                #endregion

                User newUser = new() {
                    Username = registeringUser.Username,
                    Password = registeringUser.Password, //TODO Hash password
                    Email = registeringUser.Email
                };

                string accessToken;

                using var transaction = _dbcontext.Database.BeginTransaction();

                try {
                    
                    _dbcontext.Users.Add(newUser);

                    await _dbcontext.SaveChangesAsync();

                    RefreshToken userRefreshToken = new() {
                        JwtId = Guid.NewGuid().ToString(),
                        ExpirationDate = DateTime.UtcNow.AddDays(7),
                        CreationDate = DateTime.UtcNow,
                        UserId = newUser.Id
                    };
                    
                    accessToken = _tokenService.GenerateJwtToken(newUser, role: "User");

                    _dbcontext.Tokens.Add(userRefreshToken);
                    await _dbcontext.SaveChangesAsync();
                    
                    transaction.Commit();           

                    _tokenService.SetResponseTokens(forUser: newUser, context: Response, refreshToken: userRefreshToken);

                } catch (DbUpdateException e) {
                    transaction.Rollback();
                    Debug.WriteLine(e.Message);
                    return StatusCode(500);
                }
                return Ok();
            }

            [AllowAnonymous]
            [HttpPost("login")]
            public async Task<IActionResult> LoginUser([FromBody] LoginModel loggingUser)
            {                
                var queryResult = await _dbcontext.Users
                .FirstOrDefaultAsync((u) => u.Username == loggingUser.Username && u.Password == loggingUser.Password);
                
                if(queryResult == null)
                {
                    return Unauthorized("Invalid username or password");
                }

                RefreshToken refreshToken = new() {
                    JwtId = Guid.NewGuid().ToString(),
                    ExpirationDate = DateTime.UtcNow.AddDays(7),
                    CreationDate = DateTime.UtcNow, 
                    UserId = queryResult.Id
                }; 

                _tokenService.SetResponseTokens(forUser: queryResult, context: Response, refreshToken: refreshToken);

                return  Ok();
            }

            [Authorize(Policy = "RefreshExpiredToken")] 
            [HttpPost("refresh")]
            public IActionResult RefreshToken() //! Only the refresh token should be sent here 
            {

                //TODO Add refresh token to db and link it to a user

                //? Check if the refresh token exists in the db
                //? If it does, check if it's expired
                //? If it's not expired, generate a new access token and return it
                //? If it is expired, delete it from the db and return a 401
                //? If it doesn't exist, return a 401
                //? If the user is not found, return a 401
                
/*                 _dbcontext.Tokens.FirstOrDefaultAsync((t) => t.JwtId == refreshTokenId); */

                var encodedRefreshToken = Request.Cookies["X-Refresh-Token"];
                _tokenService.DeserializeRefreshToken(encodedRefreshToken!, out var refreshToken);

                var queryResult = _dbcontext.Tokens.FirstOrDefaultAsync((t) => t.JwtId == refreshToken.JwtId);
                
                if(queryResult != null)
                {
                    
                    using var transaction = _dbcontext.Database.BeginTransaction();
                    try 
                    {
                        
                    }
                    catch(Exception){
                        transaction.Rollback();
                    }

                }

                _dbcontext.Tokens.Add(new RefreshToken() {
                    JwtId = Guid.NewGuid().ToString(),
                    ExpirationDate = DateTime.UtcNow.AddDays(7),
                    CreationDate = DateTime.UtcNow, 
                    UserId = 1
                });   



                return Ok();
            }
        }
    }