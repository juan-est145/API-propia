using Microsoft.EntityFrameworkCore;
using API_propia.DataAccess;

var builder = WebApplication.CreateBuilder(args);

const string DBConnectionName = "API-Hotel";
var connectionString = builder.Configuration.GetConnectionString(DBConnectionName);

builder.Services.AddDbContext<HotelDBContext>(x => x.UseSqlServer(connectionString));

//Add Service of JWT Authorization
//To do
//builder.Services.AddJwtTokenServices(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//To do: Config Swagger to take care of Authorization of JWT
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
