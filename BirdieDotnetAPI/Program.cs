using BirdieDotnetAPI.Data;
using BirdieDotnetAPI.Hubs;
using BirdieDotnetAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Text;

var builder = WebApplication.CreateBuilder();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<TestContext>( optionsAction: options => {
    options.UseMySql(connectionString, ServerVersion.Parse("10.4.28-mariadb"));
});

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddScoped<TokenService>();
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
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




/* builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => {
    options.Password.RequiredLength = 12;
    options.Password.RequireDigit = true;
    options.SignIn.RequireConfirmedEmail = true;
}).AddEntityFrameworkStores<TestContext>(); */

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

builder.Services.AddAuthorization(options => {
    options.AddPolicy("RefreshToken", policy => {
        policy.RequireAssertion(context => {
            var httpContext = context.Resource as HttpContext;
        
            return httpContext!.Request.Cookies.ContainsKey("X-Refresh-Token");
        }); 
    });

});
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapHub<ChatHub>("/chathub").RequireAuthorization();
app.MapControllers(); 
app.Run(); 