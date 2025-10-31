using FluentValidation;
using Cp1.Application.DTOs;

namespace Cp1.Application.Validations;

public class UpdateEspecialidadeDtoValidator : AbstractValidator<UpdateEspecialidadeDto>
{
    public UpdateEspecialidadeDtoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome da especialidade é obrigatório.")
            .MinimumLength(3).WithMessage("O nome deve ter pelo menos 3 caracteres.")
            .MaximumLength(100).WithMessage("O nome não pode ter mais de 100 caracteres.");

        RuleFor(x => x.Descricao)
            .MaximumLength(500).WithMessage("A descrição não pode ter mais de 500 caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.Descricao));
    }
}

