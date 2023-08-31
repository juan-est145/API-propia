using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using API_propia.DataAccess;
using API_propia;

var builder = WebApplication.CreateBuilder(args);

const string DBConnectionName = "API-Hotel";
var connectionString = builder.Configuration.GetConnectionString(DBConnectionName);

builder.Services.AddDbContext<HotelDBContext>(x => x.UseSqlServer(connectionString));

//Add Service of JWT Authorization
builder.Services.AddJwtServices(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserOnlyPolicy", policy => policy.RequireClaim("UserOnly"));
    options.AddPolicy("AdminOnlyPolicy", policy => policy.RequireClaim("AdminOnly"));
});

builder.Services.AddEndpointsApiExplorer();

//To do: Config Swagger to take care of Authorization of JWT
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization Header using Bearer Scheme"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[]{}
        }
        
    });
});

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
