using Microsoft.EntityFrameworkCore;
using Project2Backend.Components;
using Project2Backend.Context;
using Project2Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region EntityFrameWork
var connectionString = builder.Configuration.GetConnectionString("Project2ConnectionStr");
builder.Services.AddDbContext<Project2Context>(x => x.UseSqlServer(connectionString));
#endregion

#region Services
builder.Services.AddScoped<UserService>();
#endregion

#region Components
builder.Services.AddScoped<UserComponent>();
#endregion

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();


app.Run();