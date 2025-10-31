namespace Cp1.Application.DTOs;

// DTO para resposta (GET)
public class MedicoDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Crm { get; set; } = string.Empty;
    public string? Telefone { get; set; }
    public string? Email { get; set; }
}

// DTO para criação (POST)
public class CreateMedicoDto
{
    public string Nome { get; set; } = string.Empty;
    public string Crm { get; set; } = string.Empty;
    public string? Telefone { get; set; }
    public string? Email { get; set; }
}

// DTO para atualização (PUT)
public class UpdateMedicoDto
{
    public string Nome { get; set; } = string.Empty;
    public string Crm { get; set; } = string.Empty;
    public string? Telefone { get; set; }
    public string? Email { get; set; }
}

