using Cp1.Application.DTOs;

namespace Cp1.Application.Services;

public interface IPacienteService
{
    Task<IEnumerable<PacienteDto>> GetAllAsync();
    Task<PacienteDto?> GetByIdAsync(int id);
    Task<PacienteDto> CreateAsync(CreatePacienteDto createDto);
    Task<PacienteDto?> UpdateAsync(int id, UpdatePacienteDto updateDto);
    Task<bool> DeleteAsync(int id);
}

