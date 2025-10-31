namespace Cp1.Domain.Entities;

public class Medico
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Crm { get; set; } = string.Empty;
    public string? Telefone { get; set; }
    public string? Email { get; set; }
    
    // Relacionamento: Um médico pode ter várias consultas
    public ICollection<Consulta> Consultas { get; set; } = new List<Consulta>();
}

