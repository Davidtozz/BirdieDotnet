using BirdieDotnetAPI.Data;
using BirdieDotnetAPI.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//! Replacing in favor of EntityFramework Core
/* builder.Services.AddSingleton(new MySqlConnection(connectionString)); */

builder.Services.AddDbContext<TestContext>( optionsAction: options => {
    options.UseMySql(connectionString, ServerVersion.Parse("10.4.28-mariadb"));
});


//TODO Configure Identity properly
/* builder.Services.AddIdentity<User,IdentityRole>()
    .AddEntityFrameworkStores<TestContext>()
    .AddDefaultTokenProviders(); */

//builder.Services.AddIdentity<>
//builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<TestContext>();


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
});

builder.Services.AddAuthorization();

//? jwt debug

/* builder.Services.Configure<AuthorizationOptions>(options => {
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
}); */

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{                                       //? (2) jwt debug
    endpoints.MapHub<ChatHub>("/chathub")/* .RequireAuthorization() */; 
});

app.MapControllers(); //? UserController
app.Run(); 