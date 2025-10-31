using FluentValidation;
using Cp1.Application.DTOs;
using System.Text.RegularExpressions;

namespace Cp1.Application.Validations;

public class CreatePacienteDtoValidator : AbstractValidator<CreatePacienteDto>
{
    public CreatePacienteDtoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MinimumLength(3).WithMessage("O nome deve ter pelo menos 3 caracteres.")
            .MaximumLength(100).WithMessage("O nome não pode ter mais de 100 caracteres.");

        RuleFor(x => x.DataNascimento)
            .NotEmpty().WithMessage("A data de nascimento é obrigatória.")
            .LessThan(DateTime.Now.AddYears(-1)).WithMessage("A data de nascimento não pode ser muito recente.")
            .GreaterThan(DateTime.Now.AddYears(-120)).WithMessage("A data de nascimento é inválida.");

        RuleFor(x => x.Cpf)
            .NotEmpty().WithMessage("O CPF é obrigatório.")
            .Must(ValidarCpf).WithMessage("CPF inválido.");

        RuleFor(x => x.Telefone)
            .MaximumLength(20).WithMessage("O telefone não pode ter mais de 20 caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.Telefone));

        RuleFor(x => x.Endereco)
            .MaximumLength(200).WithMessage("O endereço não pode ter mais de 200 caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.Endereco));
    }

    private static bool ValidarCpf(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return false;

        cpf = Regex.Replace(cpf, @"[^\d]", "");

        if (cpf.Length != 11)
            return false;

        if (cpf.Distinct().Count() == 1)
            return false;

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
}

