using FluentValidation;
using Cp1.Application.DTOs;

namespace Cp1.Application.Validations;

public class CreateConsultaDtoValidator : AbstractValidator<CreateConsultaDto>
{
    public CreateConsultaDtoValidator()
    {
        RuleFor(x => x.DataHora)
            .NotEmpty().WithMessage("A data e hora são obrigatórias.")
            .GreaterThan(DateTime.Now.AddHours(1)).WithMessage("A consulta deve ser agendada com pelo menos 1 hora de antecedência.");

        RuleFor(x => x.PacienteId)
            .GreaterThan(0).WithMessage("O paciente é obrigatório.");

        RuleFor(x => x.MedicoId)
            .GreaterThan(0).WithMessage("O médico é obrigatório.");

        RuleFor(x => x.EspecialidadeId)
            .GreaterThan(0).WithMessage("A especialidade é obrigatória.");

        RuleFor(x => x.Observacoes)
            .MaximumLength(1000).WithMessage("As observações não podem ter mais de 1000 caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.Observacoes));
    }
}

