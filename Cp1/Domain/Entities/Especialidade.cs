namespace Cp1.Domain.Entities;

public class Especialidade
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    
    // Relacionamento: Uma especialidade pode ter várias consultas
    public ICollection<Consulta> Consultas { get; set; } = new List<Consulta>();
}

