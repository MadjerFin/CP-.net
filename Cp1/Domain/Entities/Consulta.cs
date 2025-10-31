namespace Cp1.Domain.Entities;

public class Consulta
{
    public int Id { get; set; }
    public DateTime DataHora { get; set; }
    public char Status { get; set; } // A = Agendada, C = Cancelada, R = Realizada
    public string? Observacoes { get; set; }
    
    // Chaves estrangeiras
    public int PacienteId { get; set; }
    public int MedicoId { get; set; }
    public int EspecialidadeId { get; set; }
    
    // Relacionamentos de navegação
    public Paciente Paciente { get; set; } = null!;
    public Medico Medico { get; set; } = null!;
    public Especialidade Especialidade { get; set; } = null!;
    
    // Métodos de negócio (entidade rica)
    public void Cancelar()
    {
        if (Status == 'R')
            throw new InvalidOperationException("Não é possível cancelar uma consulta já realizada.");
        
        Status = 'C';
    }
    
    public void Realizar()
    {
        if (Status == 'C')
            throw new InvalidOperationException("Não é possível realizar uma consulta cancelada.");
        
        Status = 'R';
    }
    
    public bool PodeSerCancelada()
    {
        return Status == 'A';
    }
}

