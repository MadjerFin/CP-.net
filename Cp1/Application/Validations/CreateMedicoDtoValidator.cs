using FluentValidation;
using Cp1.Application.DTOs;
using System.Text.RegularExpressions;

namespace Cp1.Application.Validations;

public class CreateMedicoDtoValidator : AbstractValidator<CreateMedicoDto>
{
    public CreateMedicoDtoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MinimumLength(3).WithMessage("O nome deve ter pelo menos 3 caracteres.")
            .MaximumLength(100).WithMessage("O nome não pode ter mais de 100 caracteres.");

        RuleFor(x => x.Crm)
            .NotEmpty().WithMessage("O CRM é obrigatório.")
            .Must(ValidarCrm).WithMessage("CRM inválido. Deve conter apenas números com 4 a 8 dígitos.");

        RuleFor(x => x.Telefone)
            .MaximumLength(20).WithMessage("O telefone não pode ter mais de 20 caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.Telefone));

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("E-mail inválido.")
            .MaximumLength(100).WithMessage("O e-mail não pode ter mais de 100 caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.Email));
    }

    private static bool ValidarCrm(string crm)
    {
        if (string.IsNullOrWhiteSpace(crm))
            return false;

        crm = Regex.Replace(crm, @"[^\d]", "");

        return crm.Length >= 4 && crm.Length <= 8;
    }
}

