namespace Cp1.Application.DTOs;

// DTO para resposta (GET)
public class EspecialidadeDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
}

// DTO para criação (POST)
public class CreateEspecialidadeDto
{
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
}

// DTO para atualização (PUT)
public class UpdateEspecialidadeDto
{
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
}

