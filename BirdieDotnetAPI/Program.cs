using BirdieDotnetAPI.Hubs;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton(new MySqlConnection(connectionString));

builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("https://example.com")
                .AllowAnyHeader()
                .WithMethods("GET", "POST")
                .AllowCredentials();
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.UseRouting();

app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseCors();

app.MapHub<ChatHub>("/chathub");

app.MapControllers();

app.Run();


