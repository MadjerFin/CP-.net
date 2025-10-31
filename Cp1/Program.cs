using Microsoft.EntityFrameworkCore;
using Cp1.Infrastructure.Data;
using Cp1.Application.Mappings;
using Cp1.Application.Validations;
using FluentValidation.AspNetCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configurar Entity Framework com Oracle
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseOracle(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        oracleOptions => oracleOptions.UseOracleSQLCompatibility("11")));

// Configurar AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Configurar FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CreatePacienteDtoValidator>();
builder.Services.AddFluentValidationAutoValidation();

// Registrar Services
builder.Services.AddScoped<IPacienteService, PacienteService>();
builder.Services.AddScoped<IConsultaService, ConsultaService>();

// Configurar Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi(options =>
{
    options.AddDocument(new()
    {
        Info = new()
        {
            Title = "API de Agendamento de Consultas Médicas",
            Version = "v1",
            Description = "API RESTful para gerenciamento de consultas médicas, pacientes, médicos e especialidades.",
            Contact = new()
            {
                Name = "Equipe de Desenvolvimento",
            }
        }
    });
});

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
