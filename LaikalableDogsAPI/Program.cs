using LaikableDogsAPI.DataAccess;
using LaikableDogsAPI.DataAccess.Interfaces;
using LaikableDogsAPI.Models;
using LaikableDogsAPI.Services;
using LaikableDogsAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// MongoDb Config
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("DatabaseSettings"));

// Custom services
builder.Services.AddTransient<IDogProvider, DogProvider>();
builder.Services.AddTransient<IAuxProvider, AuxProvider>();
builder.Services.AddTransient<IDogService, DogService>();
builder.Services.AddTransient<IAuxService, AuxService>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
