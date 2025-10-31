using System.ComponentModel.DataAnnotations;

namespace Cp1.Entities;

public class Consulta
{
    public int Id {get; set;}
    public DateTime Data {get; set;}
    public TimestampAttribute Hora {get; set;}

    // Enum para o status da consulta


    public char Status {get; set;} // A = Agendada, C = Cancelada, R = Realizada
    public string Observacoes  {get; set;} 
    
}