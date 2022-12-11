global using KontaktyBackend.Data;
global using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy",
        builder =>
        {
            builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins("https://localhost:3001", "https://localhost:3000"); //CORSpolicy dla lokalnej instancji react appki (port 3001 oraz 3000 bo czasami 3000 jest zajêty), do zdeployowanej dodaæ dodatkowy adres
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

app.UseCors("CORSPolicy"); //u¿ycie cors

app.UseAuthorization();

app.MapControllers();

app.Run();
