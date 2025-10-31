using Microsoft.AspNetCore.Mvc;
using Cp1.Application.DTOs;
using Cp1.Application.Services;

namespace Cp1.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PacientesController : ControllerBase
{
    private readonly IPacienteService _pacienteService;

    public PacientesController(IPacienteService pacienteService)
    {
        _pacienteService = pacienteService;
    }

    /// <summary>
    /// Lista todos os pacientes
    /// </summary>
    /// <returns>Lista de pacientes</returns>
    /// <response code="200">Retorna a lista de pacientes</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PacienteDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PacienteDto>>> GetAll()
    {
        var pacientes = await _pacienteService.GetAllAsync();
        return Ok(pacientes);
    }

    /// <summary>
    /// Busca um paciente por ID
    /// </summary>
    /// <param name="id">ID do paciente</param>
    /// <returns>Dados do paciente</returns>
    /// <response code="200">Retorna o paciente encontrado</response>
    /// <response code="404">Paciente não encontrado</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PacienteDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PacienteDto>> GetById(int id)
    {
        var paciente = await _pacienteService.GetByIdAsync(id);
        
        if (paciente == null)
            return NotFound(new { message = "Paciente não encontrado." });

        return Ok(paciente);
    }

    /// <summary>
    /// Cria um novo paciente
    /// </summary>
    /// <param name="createDto">Dados do paciente</param>
    /// <returns>Paciente criado</returns>
    /// <response code="201">Paciente criado com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    [HttpPost]
    [ProducesResponseType(typeof(PacienteDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PacienteDto>> Create([FromBody] CreatePacienteDto createDto)
    {
        try
        {
            var paciente = await _pacienteService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = paciente.Id }, paciente);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Atualiza um paciente existente
    /// </summary>
    /// <param name="id">ID do paciente</param>
    /// <param name="updateDto">Dados atualizados do paciente</param>
    /// <returns>Paciente atualizado</returns>
    /// <response code="200">Paciente atualizado com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    /// <response code="404">Paciente não encontrado</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(PacienteDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PacienteDto>> Update(int id, [FromBody] UpdatePacienteDto updateDto)
    {
        try
        {
            var paciente = await _pacienteService.UpdateAsync(id, updateDto);
            
            if (paciente == null)
                return NotFound(new { message = "Paciente não encontrado." });

            return Ok(paciente);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Exclui um paciente
    /// </summary>
    /// <param name="id">ID do paciente</param>
    /// <returns>Sem conteúdo</returns>
    /// <response code="204">Paciente excluído com sucesso</response>
    /// <response code="400">Não é possível excluir paciente com consultas</response>
    /// <response code="404">Paciente não encontrado</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var deleted = await _pacienteService.DeleteAsync(id);
            
            if (!deleted)
                return NotFound(new { message = "Paciente não encontrado." });

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}


