using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TodoList_Base.Database;
using TodoListApp.Mapping;
using TodoListApp.Repository;
using TodoListApp.Services;
using TodoListApp.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

string ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(ConnectionString));



#region Auto Mapper
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new Mapping());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion


#region  Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Logging.ClearProviders(); // Remove default logging providers
builder.Logging.AddSerilog();    // Add Serilog
#endregion


// Add services to the container.
builder.Services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
builder.Services.AddTransient(typeof(IDeleteRepository<>), typeof(DeleteRepository<>));
builder.Services.AddTransient<ITodoItemService, TodoItemServicesImpl>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
