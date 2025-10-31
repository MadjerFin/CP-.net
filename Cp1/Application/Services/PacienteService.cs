using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Cp1.Application.DTOs;
using Cp1.Domain.Entities;
using Cp1.Infrastructure.Data;

namespace Cp1.Application.Services;

public class PacienteService : IPacienteService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public PacienteService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PacienteDto>> GetAllAsync()
    {
        var pacientes = await _context.Pacientes.ToListAsync();
        return _mapper.Map<IEnumerable<PacienteDto>>(pacientes);
    }

    public async Task<PacienteDto?> GetByIdAsync(int id)
    {
        var paciente = await _context.Pacientes.FindAsync(id);
        return paciente == null ? null : _mapper.Map<PacienteDto>(paciente);
    }

    public async Task<PacienteDto> CreateAsync(CreatePacienteDto createDto)
    {
        var paciente = _mapper.Map<Paciente>(createDto);
        paciente.Validar();
        
        _context.Pacientes.Add(paciente);
        await _context.SaveChangesAsync();
        
        return _mapper.Map<PacienteDto>(paciente);
    }

    public async Task<PacienteDto?> UpdateAsync(int id, UpdatePacienteDto updateDto)
    {
        var paciente = await _context.Pacientes.FindAsync(id);
        if (paciente == null)
            return null;

        _mapper.Map(updateDto, paciente);
        paciente.Validar();
        
        await _context.SaveChangesAsync();
        
        return _mapper.Map<PacienteDto>(paciente);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var paciente = await _context.Pacientes.FindAsync(id);
        if (paciente == null)
            return false;

        // Verificar se há consultas relacionadas
        var temConsultas = await _context.Consultas.AnyAsync(c => c.PacienteId == id);
        if (temConsultas)
            throw new InvalidOperationException("Não é possível excluir um paciente que possui consultas agendadas.");

        _context.Pacientes.Remove(paciente);
        await _context.SaveChangesAsync();
        
        return true;
    }
}

