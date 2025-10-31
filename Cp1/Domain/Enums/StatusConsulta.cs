namespace Cp1.Domain.Enums;

public enum StatusConsulta
{
    Agendada = 'A',
    Cancelada = 'C',
    Realizada = 'R'
}

public static class StatusConsultaExtensions
{
    public static char ToChar(this StatusConsulta status)
    {
        return (char)status;
    }
    
    public static StatusConsulta FromChar(char status)
    {
        return status switch
        {
            'A' => StatusConsulta.Agendada,
            'C' => StatusConsulta.Cancelada,
            'R' => StatusConsulta.Realizada,
            _ => throw new ArgumentException($"Status inválido: {status}")
        };
    }
    
    public static string ToString(StatusConsulta status)
    {
        return status switch
        {
            StatusConsulta.Agendada => "Agendada",
            StatusConsulta.Cancelada => "Cancelada",
            StatusConsulta.Realizada => "Realizada",
            _ => "Desconhecido"
        };
    }
}
