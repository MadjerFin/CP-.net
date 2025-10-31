using Microsoft.EntityFrameworkCore;
using Oracle.EntityFrameworkCore;
using Cp1.Infrastructure.Data;
using Cp1.Application.Mappings;
using Cp1.Application.Validations;
using Cp1.Application.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configurar Entity Framework com Oracle
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseOracle(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        oracleOptions => {}));

// Configurar AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Configurar FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CreatePacienteDtoValidator>();
builder.Services.AddFluentValidationAutoValidation();

// Registrar Services
builder.Services.AddScoped<IPacienteService, PacienteService>();
builder.Services.AddScoped<IConsultaService, ConsultaService>();

// Configurar Swagger/OpenAPI (usando Microsoft.AspNetCore.OpenApi)
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
// OpenAPI será gerado automaticamente com os endpoints

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
