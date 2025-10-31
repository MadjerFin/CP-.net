using Microsoft.AspNetCore.Mvc;
using Cp1.Application.DTOs;
using Cp1.Application.Services;

namespace Cp1.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ConsultasController : ControllerBase
{
    private readonly IConsultaService _consultaService;

    public ConsultasController(IConsultaService consultaService)
    {
        _consultaService = consultaService;
    }

    /// <summary>
    /// Lista todas as consultas
    /// </summary>
    /// <returns>Lista de consultas</returns>
    /// <response code="200">Retorna a lista de consultas</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ConsultaResumoDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ConsultaResumoDto>>> GetAll()
    {
        var consultas = await _consultaService.GetAllAsync();
        return Ok(consultas);
    }

    /// <summary>
    /// Busca uma consulta por ID
    /// </summary>
    /// <param name="id">ID da consulta</param>
    /// <returns>Dados da consulta</returns>
    /// <response code="200">Retorna a consulta encontrada</response>
    /// <response code="404">Consulta não encontrada</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ConsultaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ConsultaDto>> GetById(int id)
    {
        var consulta = await _consultaService.GetByIdAsync(id);
        
        if (consulta == null)
            return NotFound(new { message = "Consulta não encontrada." });

        return Ok(consulta);
    }

    /// <summary>
    /// Cria uma nova consulta
    /// </summary>
    /// <param name="createDto">Dados da consulta</param>
    /// <returns>Consulta criada</returns>
    /// <response code="201">Consulta criada com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    [HttpPost]
    [ProducesResponseType(typeof(ConsultaDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ConsultaDto>> Create([FromBody] CreateConsultaDto createDto)
    {
        try
        {
            var consulta = await _consultaService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = consulta.Id }, consulta);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Atualiza uma consulta existente
    /// </summary>
    /// <param name="id">ID da consulta</param>
    /// <param name="updateDto">Dados atualizados da consulta</param>
    /// <returns>Consulta atualizada</returns>
    /// <response code="200">Consulta atualizada com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    /// <response code="404">Consulta não encontrada</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ConsultaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ConsultaDto>> Update(int id, [FromBody] UpdateConsultaDto updateDto)
    {
        try
        {
            var consulta = await _consultaService.UpdateAsync(id, updateDto);
            
            if (consulta == null)
                return NotFound(new { message = "Consulta não encontrada." });

            return Ok(consulta);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Exclui uma consulta
    /// </summary>
    /// <param name="id">ID da consulta</param>
    /// <returns>Sem conteúdo</returns>
    /// <response code="204">Consulta excluída com sucesso</response>
    /// <response code="404">Consulta não encontrada</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _consultaService.DeleteAsync(id);
        
        if (!deleted)
            return NotFound(new { message = "Consulta não encontrada." });

        return NoContent();
    }

    /// <summary>
    /// Cancela uma consulta agendada
    /// </summary>
    /// <param name="id">ID da consulta</param>
    /// <returns>Consulta cancelada</returns>
    /// <response code="200">Consulta cancelada com sucesso</response>
    /// <response code="400">Não é possível cancelar a consulta</response>
    /// <response code="404">Consulta não encontrada</response>
    [HttpPost("{id}/cancelar")]
    [ProducesResponseType(typeof(ConsultaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ConsultaDto>> Cancelar(int id)
    {
        try
        {
            var consulta = await _consultaService.CancelarAsync(id);
            
            if (consulta == null)
                return NotFound(new { message = "Consulta não encontrada." });

            return Ok(consulta);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Marca uma consulta como realizada
    /// </summary>
    /// <param name="id">ID da consulta</param>
    /// <returns>Consulta realizada</returns>
    /// <response code="200">Consulta marcada como realizada com sucesso</response>
    /// <response code="400">Não é possível realizar a consulta</response>
    /// <response code="404">Consulta não encontrada</response>
    [HttpPost("{id}/realizar")]
    [ProducesResponseType(typeof(ConsultaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ConsultaDto>> Realizar(int id)
    {
        try
        {
            var consulta = await _consultaService.RealizarAsync(id);
            
            if (consulta == null)
                return NotFound(new { message = "Consulta não encontrada." });

            return Ok(consulta);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Reagenda uma consulta agendada
    /// </summary>
    /// <param name="id">ID da consulta</param>
    /// <param name="reagendarDto">Nova data/hora da consulta</param>
    /// <returns>Consulta reagendada</returns>
    /// <response code="200">Consulta reagendada com sucesso</response>
    /// <response code="400">Não é possível reagendar a consulta</response>
    /// <response code="404">Consulta não encontrada</response>
    [HttpPost("{id}/reagendar")]
    [ProducesResponseType(typeof(ConsultaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ConsultaDto>> Reagendar(int id, [FromBody] ReagendarConsultaDto reagendarDto)
    {
        try
        {
            var consulta = await _consultaService.ReagendarAsync(id, reagendarDto);
            
            if (consulta == null)
                return NotFound(new { message = "Consulta não encontrada." });

            return Ok(consulta);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}

