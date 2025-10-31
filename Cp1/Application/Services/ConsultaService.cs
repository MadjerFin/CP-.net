using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Cp1.Application.DTOs;
using Cp1.Domain.Entities;
using Cp1.Infrastructure.Data;

namespace Cp1.Application.Services;

public class ConsultaService : IConsultaService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ConsultaService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ConsultaResumoDto>> GetAllAsync()
    {
        var consultas = await _context.Consultas
            .Include(c => c.Paciente)
            .Include(c => c.Medico)
            .Include(c => c.Especialidade)
            .OrderBy(c => c.DataHora)
            .ToListAsync();
        
        return _mapper.Map<IEnumerable<ConsultaResumoDto>>(consultas);
    }

    public async Task<ConsultaDto?> GetByIdAsync(int id)
    {
        var consulta = await _context.Consultas
            .Include(c => c.Paciente)
            .Include(c => c.Medico)
            .Include(c => c.Especialidade)
            .FirstOrDefaultAsync(c => c.Id == id);
        
        return consulta == null ? null : _mapper.Map<ConsultaDto>(consulta);
    }

    public async Task<ConsultaDto> CreateAsync(CreateConsultaDto createDto)
    {
        // Verificar se paciente existe
        var paciente = await _context.Pacientes.FirstOrDefaultAsync(p => p.Id == createDto.PacienteId);
        if (paciente == null)
            throw new ArgumentException("Paciente não encontrado.");

        // Verificar se médico existe
        var medico = await _context.Medicos.FirstOrDefaultAsync(m => m.Id == createDto.MedicoId);
        if (medico == null)
            throw new ArgumentException("Médico não encontrado.");

        // Verificar se especialidade existe
        var especialidade = await _context.Especialidades.FirstOrDefaultAsync(e => e.Id == createDto.EspecialidadeId);
        if (especialidade == null)
            throw new ArgumentException("Especialidade não encontrada.");

        var consulta = _mapper.Map<Consulta>(createDto);
        consulta.Validar();
        
        _context.Consultas.Add(consulta);
        await _context.SaveChangesAsync();
        
        // Recarregar com relacionamentos
        await _context.Entry(consulta)
            .Reference(c => c.Paciente).LoadAsync();
        await _context.Entry(consulta)
            .Reference(c => c.Medico).LoadAsync();
        await _context.Entry(consulta)
            .Reference(c => c.Especialidade).LoadAsync();
        
        return _mapper.Map<ConsultaDto>(consulta);
    }

    public async Task<ConsultaDto?> UpdateAsync(int id, UpdateConsultaDto updateDto)
    {
        var consulta = await _context.Consultas
            .Include(c => c.Paciente)
            .Include(c => c.Medico)
            .Include(c => c.Especialidade)
            .FirstOrDefaultAsync(c => c.Id == id);
        
        if (consulta == null)
            return null;

        // Verificar se paciente existe
        var paciente = await _context.Pacientes.FirstOrDefaultAsync(p => p.Id == updateDto.PacienteId);
        if (paciente == null)
            throw new ArgumentException("Paciente não encontrado.");

        // Verificar se médico existe
        var medico = await _context.Medicos.FirstOrDefaultAsync(m => m.Id == updateDto.MedicoId);
        if (medico == null)
            throw new ArgumentException("Médico não encontrado.");

        // Verificar se especialidade existe
        var especialidade = await _context.Especialidades.FirstOrDefaultAsync(e => e.Id == updateDto.EspecialidadeId);
        if (especialidade == null)
            throw new ArgumentException("Especialidade não encontrada.");

        _mapper.Map(updateDto, consulta);
        consulta.Validar();
        
        await _context.SaveChangesAsync();
        
        return _mapper.Map<ConsultaDto>(consulta);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var consulta = await _context.Consultas.FindAsync(id);
        if (consulta == null)
            return false;

        _context.Consultas.Remove(consulta);
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<ConsultaDto?> CancelarAsync(int id)
    {
        var consulta = await _context.Consultas
            .Include(c => c.Paciente)
            .Include(c => c.Medico)
            .Include(c => c.Especialidade)
            .FirstOrDefaultAsync(c => c.Id == id);
        
        if (consulta == null)
            return null;

        consulta.Cancelar();
        await _context.SaveChangesAsync();
        
        return _mapper.Map<ConsultaDto>(consulta);
    }

    public async Task<ConsultaDto?> RealizarAsync(int id)
    {
        var consulta = await _context.Consultas
            .Include(c => c.Paciente)
            .Include(c => c.Medico)
            .Include(c => c.Especialidade)
            .FirstOrDefaultAsync(c => c.Id == id);
        
        if (consulta == null)
            return null;

        consulta.Realizar();
        await _context.SaveChangesAsync();
        
        return _mapper.Map<ConsultaDto>(consulta);
    }

    public async Task<ConsultaDto?> ReagendarAsync(int id, ReagendarConsultaDto reagendarDto)
    {
        var consulta = await _context.Consultas
            .Include(c => c.Paciente)
            .Include(c => c.Medico)
            .Include(c => c.Especialidade)
            .FirstOrDefaultAsync(c => c.Id == id);
        
        if (consulta == null)
            return null;

        consulta.Reagendar(reagendarDto.NovaDataHora);
        await _context.SaveChangesAsync();
        
        return _mapper.Map<ConsultaDto>(consulta);
    }
}

