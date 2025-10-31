namespace Cp1.Domain.Entities;

public class Paciente
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public string Cpf { get; set; } = string.Empty;
    public string? Telefone { get; set; }
    public string? Endereco { get; set; }
    
    // Relacionamento: Um paciente pode ter várias consultas
    public ICollection<Consulta> Consultas { get; set; } = new List<Consulta>();
}

