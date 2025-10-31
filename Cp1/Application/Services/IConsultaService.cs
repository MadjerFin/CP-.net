using Cp1.Application.DTOs;

namespace Cp1.Application.Services;

public interface IConsultaService
{
    Task<IEnumerable<ConsultaResumoDto>> GetAllAsync();
    Task<ConsultaDto?> GetByIdAsync(int id);
    Task<ConsultaDto> CreateAsync(CreateConsultaDto createDto);
    Task<ConsultaDto?> UpdateAsync(int id, UpdateConsultaDto updateDto);
    Task<bool> DeleteAsync(int id);
    Task<ConsultaDto?> CancelarAsync(int id);
    Task<ConsultaDto?> RealizarAsync(int id);
    Task<ConsultaDto?> ReagendarAsync(int id, ReagendarConsultaDto reagendarDto);
}

