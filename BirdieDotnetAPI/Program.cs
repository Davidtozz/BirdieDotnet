using BirdieDotnetAPI.Data;
using BirdieDotnetAPI.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

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

//? Remove Server response header
builder.WebHost.UseKestrel(options => {
    options.AddServerHeader = false;
});


//TODO encrypt JWT tokens
builder.Services.AddAuthentication(x => {
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false, //TODO validate issuer in production
        ValidateAudience = false, //TODO validate audience in production
        ValidateIssuerSigningKey = true, 
        ValidIssuer = builder.Configuration["Jwt:Issuer"], //dev: localhost
        ValidAudience = builder.Configuration["Jwt:Audience"], //dev: localhost
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {

            if (context.Request.Cookies.ContainsKey("X-Access-Token"))
            {
                context.Token = context.Request.Cookies["X-Access-Token"];
            }

            return Task.CompletedTask;
        }
    };


});

builder.Services.AddAuthorization();
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapHub<ChatHub>("/chathub").RequireAuthorization();

app.MapControllers(); 
app.Run(); 