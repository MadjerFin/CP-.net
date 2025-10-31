using FluentValidation;
using Cp1.Application.DTOs;

namespace Cp1.Application.Validations;

public class ReagendarConsultaDtoValidator : AbstractValidator<ReagendarConsultaDto>
{
    public ReagendarConsultaDtoValidator()
    {
        RuleFor(x => x.NovaDataHora)
            .NotEmpty().WithMessage("A nova data e hora são obrigatórias.")
            .GreaterThan(DateTime.Now.AddHours(1)).WithMessage("A nova data/hora deve ser com pelo menos 1 hora de antecedência.");
    }
}

