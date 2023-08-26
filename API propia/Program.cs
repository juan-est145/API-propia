using Microsoft.EntityFrameworkCore;
using API_propia.DataAccess;

var builder = WebApplication.CreateBuilder(args);

const string DBConnectionName = "API-Hotel";
var connectionString = builder.Configuration.GetConnectionString(DBConnectionName);

builder.Services.AddDbContext<HotelDBContext>(x => x.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
