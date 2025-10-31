namespace Cp1.Application.DTOs;

// DTO para resposta (GET)
public class PacienteDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public string Cpf { get; set; } = string.Empty;
    public string? Telefone { get; set; }
    public string? Endereco { get; set; }
    public int Idade { get; set; }
}

// DTO para criação (POST)
public class CreatePacienteDto
{
    public string Nome { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public string Cpf { get; set; } = string.Empty;
    public string? Telefone { get; set; }
    public string? Endereco { get; set; }
}

// DTO para atualização (PUT)
public class UpdatePacienteDto
{
    public string Nome { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public string Cpf { get; set; } = string.Empty;
    public string? Telefone { get; set; }
    public string? Endereco { get; set; }
}

