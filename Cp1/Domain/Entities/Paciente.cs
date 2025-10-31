using System.Text.RegularExpressions;

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
    
    // Validações e regras de negócio
    public void Validar()
    {
        if (string.IsNullOrWhiteSpace(Nome))
            throw new ArgumentException("O nome do paciente é obrigatório.");
        
        if (Nome.Length < 3 || Nome.Length > 100)
            throw new ArgumentException("O nome do paciente deve ter entre 3 e 100 caracteres.");
        
        if (DataNascimento > DateTime.Now.AddYears(-1))
            throw new ArgumentException("A data de nascimento não pode ser no futuro ou muito recente.");
        
        if (DataNascimento < DateTime.Now.AddYears(-120))
            throw new ArgumentException("A data de nascimento é inválida.");
        
        if (string.IsNullOrWhiteSpace(Cpf))
            throw new ArgumentException("O CPF do paciente é obrigatório.");
        
        if (!ValidarCpf(Cpf))
            throw new ArgumentException("O CPF informado é inválido.");
    }
    
    private static bool ValidarCpf(string cpf)
    {
        // Remove caracteres não numéricos
        cpf = Regex.Replace(cpf, @"[^\d]", "");
        
        // Verifica se tem 11 dígitos
        if (cpf.Length != 11)
            return false;
        
        // Verifica se todos os dígitos são iguais
        if (cpf.Distinct().Count() == 1)
            return false;
        
        // Validação dos dígitos verificadores
        int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        
        string tempCpf = cpf.Substring(0, 9);
        int soma = 0;
        
        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
        
        int resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;
        
        string digito = resto.ToString();
        tempCpf += digito;
        soma = 0;
        
        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
        
        resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;
        digito += resto.ToString();
        
        return cpf.EndsWith(digito);
    }
    
    public int CalcularIdade()
    {
        var hoje = DateTime.Now;
        var idade = hoje.Year - DataNascimento.Year;
        
        if (DataNascimento.Date > hoje.AddYears(-idade))
            idade--;
        
        return idade;
    }
}
