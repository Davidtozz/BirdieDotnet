using BirdieDotnetAPI.Data;
using BirdieDotnetAPI.Hubs;
using BirdieDotnetAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder();

#pragma warning disable CS0219

string localDatabase = builder.Configuration.GetConnectionString("DefaultConnection")!;
string localDatabaseVersion = "10.4.28-mariadb";

var remoteDatabase = builder.Configuration.GetConnectionString("NanodeMysql"); //? remote instance
var remoteDatabaseVersion = "8.0.33-0ubuntu0.20.04.2";

#pragma warning restore CS0219

builder.Services.AddDbContext<TestContext>(optionsAction: options =>
{
    options.UseMySql(connectionString: localDatabase, ServerVersion.Parse(localDatabaseVersion));
});

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddScoped<TokenService>();
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("https://localhost:3000")
            .AllowAnyHeader()
            .WithMethods("GET", "POST")
            .AllowCredentials();
    });
});


//? Remove Server response header
builder.WebHost.UseKestrel(options => {
    options.AddServerHeader = false;
});

//? Configure Kestrel to use HTTPS. Disabled in development
/* builder.Services.Configure<KestrelServerOptions>(options => {
    options.Listen(IPAddress.Any, 5069, listenOptions => {
        listenOptions.UseHttps();
    });
}); */

//TODO encrypt JWT tokens
builder.Services.AddAuthentication(x => {
    
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false, //! has to be true in production
        ValidateAudience = false, //! has to be true in production
        ValidateIssuerSigningKey = true, 
        ValidIssuer = builder.Configuration["Jwt:Issuer"], //dev: localhost
        ValidAudience = builder.Configuration["Jwt:Audience"], //dev: localhost
        //ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
        ClockSkew = TimeSpan.Zero
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
        },
        OnAuthenticationFailed = context => {
            if(context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Add("Token-Expired", "true");
            }
            return Task.CompletedTask;
        }
    };
});


builder.Services.AddControllers();

var app = builder.Build();

/* app.UseHttpsRedirection(); */
app.UseAuthentication();
app.UseAuthorization();
app.MapHub<ChatHub>("/chathub").RequireAuthorization();
app.MapControllers(); 
app.Run();