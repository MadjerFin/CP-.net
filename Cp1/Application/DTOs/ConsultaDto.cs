namespace Cp1.Application.DTOs;

// DTO para resposta (GET) com informações completas
public class ConsultaDto
{
    public int Id { get; set; }
    public DateTime DataHora { get; set; }
    public char Status { get; set; }
    public string StatusDescricao { get; set; } = string.Empty;
    public string? Observacoes { get; set; }
    public int PacienteId { get; set; }
    public string PacienteNome { get; set; } = string.Empty;
    public int MedicoId { get; set; }
    public string MedicoNome { get; set; } = string.Empty;
    public string MedicoCrm { get; set; } = string.Empty;
    public int EspecialidadeId { get; set; }
    public string EspecialidadeNome { get; set; } = string.Empty;
}

// DTO para resposta simplificada (Listagem)
public class ConsultaResumoDto
{
    public int Id { get; set; }
    public DateTime DataHora { get; set; }
    public char Status { get; set; }
    public string StatusDescricao { get; set; } = string.Empty;
    public string PacienteNome { get; set; } = string.Empty;
    public string MedicoNome { get; set; } = string.Empty;
    public string EspecialidadeNome { get; set; } = string.Empty;
}

// DTO para criação (POST)
public class CreateConsultaDto
{
    public DateTime DataHora { get; set; }
    public string? Observacoes { get; set; }
    public int PacienteId { get; set; }
    public int MedicoId { get; set; }
    public int EspecialidadeId { get; set; }
}

// DTO para atualização (PUT)
public class UpdateConsultaDto
{
    public DateTime DataHora { get; set; }
    public char Status { get; set; }
    public string? Observacoes { get; set; }
    public int PacienteId { get; set; }
    public int MedicoId { get; set; }
    public int EspecialidadeId { get; set; }
}

// DTO para reagendar consulta
public class ReagendarConsultaDto
{
    public DateTime NovaDataHora { get; set; }
}

