using BirdieDotnetAPI.Data;
using BirdieDotnetAPI.Hubs;
using BirdieDotnetAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder();

// Add services to the container.



var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//! Replacing in favor of EntityFramework Core
/* builder.Services.AddSingleton(new MySqlConnection(connectionString)); */

builder.Services.AddDbContext<TestContext>( optionsAction: options => {
    options.UseMySql(connectionString, ServerVersion.Parse("10.4.28-mariadb"));
});
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);


builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("https://localhost:5069")
                .AllowAnyHeader()
                .WithMethods("GET", "POST")
                .AllowCredentials();
        });
});


//TODO encrypt JWT tokens
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
       /*  ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true, */
        ValidIssuer = builder.Configuration["Jwt:Issuer"], //dev: localhost
        ValidAudience = builder.Configuration["Jwt:Audience"], //dev: localhost
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();


app.UseExceptionHandler("/");


app.MapHub<ChatHub>("/chathub");

app.MapControllers(); //? UserController, ConversationController 

app.Run(); 


