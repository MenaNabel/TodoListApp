using Microsoft.EntityFrameworkCore;
using TodoList_Base.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((action, config) =>
{
    config.AddJsonFile("appsettings-TodoList-Base.json");
});

// Add services to the container. ServerTestConnection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
//var connectionString = builder.Configuration.GetConnectionString("LocalConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
