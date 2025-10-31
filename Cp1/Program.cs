using Microsoft.EntityFrameworkCore;
using Cp1.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configurar Entity Framework com Oracle
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseOracle(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        oracleOptions => oracleOptions.UseOracleSQLCompatibility("11")));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
