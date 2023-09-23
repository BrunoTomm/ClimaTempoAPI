using ClimaLocal.App.Interfaces;
using ClimaLocal.App.Services;
using ClimaLocal.Client;
using ClimaLocal.Client.Interfaces;
using ClimaLocal.Data;
using ClimaLocal.Data.Repository;
using ClimaLocal.Domain.Interfaces;
using ClimaLocal.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddJsonFile("appsettings.json");

// Acessar a string de conexão
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ClimaContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddMvc();

builder.Services.AddScoped<IClimaApp, ClimaApp>();
builder.Services.AddScoped<IRestClimaTempo, RestClimaTempo>();
builder.Services.AddScoped<IPrevisaoCidadeRepository, PrevisaoCidadeRepository>();
builder.Services.AddScoped<IPrevisaoAeroportoRepository, PrevisaoAeroportoRepository>();
builder.Services.AddScoped<ILogRepository, LogRepository>();

builder.Services.AddLogging(builder =>
{
    builder.AddConsole();
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
