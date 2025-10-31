using FluentValidation;
using Cp1.Application.DTOs;

namespace Cp1.Application.Validations;

public class UpdateConsultaDtoValidator : AbstractValidator<UpdateConsultaDto>
{
    public UpdateConsultaDtoValidator()
    {
        RuleFor(x => x.DataHora)
            .NotEmpty().WithMessage("A data e hora são obrigatórias.");

        RuleFor(x => x.Status)
            .Must(status => status == 'A' || status == 'C' || status == 'R')
            .WithMessage("Status inválido. Valores aceitos: A (Agendada), C (Cancelada), R (Realizada).");

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

