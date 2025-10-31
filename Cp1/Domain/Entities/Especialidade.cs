namespace Cp1.Domain.Entities;

public class Especialidade
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    
    // Relacionamento: Uma especialidade pode ter várias consultas
    public ICollection<Consulta> Consultas { get; set; } = new List<Consulta>();
    
    // Validações e regras de negócio
    public void Validar()
    {
        if (string.IsNullOrWhiteSpace(Nome))
            throw new ArgumentException("O nome da especialidade é obrigatório.");
        
        if (Nome.Length < 3 || Nome.Length > 100)
            throw new ArgumentException("O nome da especialidade deve ter entre 3 e 100 caracteres.");
        
        if (!string.IsNullOrWhiteSpace(Descricao) && Descricao.Length > 500)
            throw new ArgumentException("A descrição da especialidade não pode ter mais de 500 caracteres.");
    }
}
