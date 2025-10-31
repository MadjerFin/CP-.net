using System.Text.RegularExpressions;

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
    
    // Validações e regras de negócio
    public void Validar()
    {
        if (string.IsNullOrWhiteSpace(Nome))
            throw new ArgumentException("O nome do médico é obrigatório.");
        
        if (Nome.Length < 3 || Nome.Length > 100)
            throw new ArgumentException("O nome do médico deve ter entre 3 e 100 caracteres.");
        
        if (string.IsNullOrWhiteSpace(Crm))
            throw new ArgumentException("O CRM do médico é obrigatório.");
        
        if (!ValidarCrm(Crm))
            throw new ArgumentException("O CRM informado é inválido. Formato esperado: números apenas.");
        
        if (!string.IsNullOrWhiteSpace(Email) && !ValidarEmail(Email))
            throw new ArgumentException("O e-mail informado é inválido.");
    }
    
    private static bool ValidarCrm(string crm)
    {
        // Remove caracteres não numéricos
        crm = Regex.Replace(crm, @"[^\d]", "");
        
        // CRM geralmente tem entre 4 e 8 dígitos
        return crm.Length >= 4 && crm.Length <= 8;
    }
    
    private static bool ValidarEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;
        
        try
        {
            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }
        catch
        {
            return false;
        }
    }
}
