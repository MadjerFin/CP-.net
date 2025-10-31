namespace Cp1.Domain.Entities;

public class Consulta
{
    public int Id { get; set; }
    public DateTime DataHora { get; set; }
    public char Status { get; set; } = 'A'; // A = Agendada, C = Cancelada, R = Realizada
    public string? Observacoes { get; set; }
    
    // Chaves estrangeiras
    public int PacienteId { get; set; }
    public int MedicoId { get; set; }
    public int EspecialidadeId { get; set; }
    
    // Relacionamentos de navegação
    public Paciente Paciente { get; set; } = null!;
    public Medico Medico { get; set; } = null!;
    public Especialidade Especialidade { get; set; } = null!;
    
    // Validações e regras de negócio
    public void Validar()
    {
        if (DataHora < DateTime.Now.AddHours(-1))
            throw new ArgumentException("A consulta não pode ser agendada no passado.");
        
        if (DataHora.Date < DateTime.Now.Date)
            throw new ArgumentException("A consulta não pode ser agendada para uma data passada.");
        
        // Permite agendar até 1 hora antes
        if (DataHora < DateTime.Now.AddHours(1))
            throw new ArgumentException("A consulta deve ser agendada com pelo menos 1 hora de antecedência.");
        
        if (PacienteId <= 0)
            throw new ArgumentException("O paciente é obrigatório.");
        
        if (MedicoId <= 0)
            throw new ArgumentException("O médico é obrigatório.");
        
        if (EspecialidadeId <= 0)
            throw new ArgumentException("A especialidade é obrigatória.");
        
        if (!string.IsNullOrWhiteSpace(Observacoes) && Observacoes.Length > 1000)
            throw new ArgumentException("As observações não podem ter mais de 1000 caracteres.");
        
        if (Status != 'A' && Status != 'C' && Status != 'R')
            throw new ArgumentException("Status inválido. Valores aceitos: A (Agendada), C (Cancelada), R (Realizada).");
    }
    
    // Métodos de negócio (entidade rica)
    public void Cancelar()
    {
        if (Status == 'R')
            throw new InvalidOperationException("Não é possível cancelar uma consulta já realizada.");
        
        if (Status == 'C')
            throw new InvalidOperationException("A consulta já está cancelada.");
        
        Status = 'C';
    }
    
    public void Realizar()
    {
        if (Status == 'C')
            throw new InvalidOperationException("Não é possível realizar uma consulta cancelada.");
        
        if (Status == 'R')
            throw new InvalidOperationException("A consulta já foi realizada.");
        
        if (DataHora > DateTime.Now)
            throw new InvalidOperationException("Não é possível realizar uma consulta que ainda não aconteceu.");
        
        Status = 'R';
    }
    
    public void Reagendar(DateTime novaDataHora)
    {
        if (Status == 'R')
            throw new InvalidOperationException("Não é possível reagendar uma consulta já realizada.");
        
        if (Status == 'C')
            throw new InvalidOperationException("Não é possível reagendar uma consulta cancelada.");
        
        if (novaDataHora < DateTime.Now.AddHours(1))
            throw new ArgumentException("A nova data/hora deve ser com pelo menos 1 hora de antecedência.");
        
        DataHora = novaDataHora;
    }
    
    public bool PodeSerCancelada()
    {
        return Status == 'A';
    }
    
    public bool PodeSerRealizada()
    {
        return Status == 'A' && DataHora <= DateTime.Now;
    }
    
    public bool EstaAgendada()
    {
        return Status == 'A';
    }
    
    public bool EstaCancelada()
    {
        return Status == 'C';
    }
    
    public bool FoiRealizada()
    {
        return Status == 'R';
    }
}
