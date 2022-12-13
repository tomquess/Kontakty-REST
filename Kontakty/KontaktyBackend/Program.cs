global using KontaktyBackend.Data;
global using Microsoft.EntityFrameworkCore;
using KontaktyBackend.Helpers;
using KontaktyBackend.Models;
using KontaktyBackend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer; //6.0.11 dla .net 6
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

{
    var services = builder.Services;
    services.AddCors();
    services.AddControllers();

    // configure strongly typed settings object
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    // configure DI for application services
    services.AddScoped<IUserService, UserService>();
}


builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy",
        builder =>
        {
            builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins("https://localhost:3000"); //CORSpolicy dla lokalnej instancji react appki (port 3001 oraz 3000 bo czasami 3000 jest zajêty), do zdeployowanej dodaæ dodatkowy adres
        }
    );
});



// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<KontaktyDbContext>(options =>   
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));  //Po³¹czenie z baz¹ danych na podstawie wczeœniej ustalonego ConnectionString
});

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

app.UseHttpsRedirection();
app.UseMiddleware<JwtMiddleware>();
app.UseCors("CORSPolicy"); //u¿ycie cors

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
