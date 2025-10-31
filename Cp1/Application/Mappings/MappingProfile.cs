using AutoMapper;
using Cp1.Domain.Entities;
using Cp1.Domain.Enums;
using Cp1.Application.DTOs;

namespace Cp1.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Mapeamentos de Paciente
        CreateMap<Paciente, PacienteDto>()
            .ForMember(dest => dest.Idade, opt => opt.MapFrom(src => src.CalcularIdade()));
        
        CreateMap<CreatePacienteDto, Paciente>();
        CreateMap<UpdatePacienteDto, Paciente>();
        
        // Mapeamentos de Médico
        CreateMap<Medico, MedicoDto>();
        CreateMap<CreateMedicoDto, Medico>();
        CreateMap<UpdateMedicoDto, Medico>();
        
        // Mapeamentos de Especialidade
        CreateMap<Especialidade, EspecialidadeDto>();
        CreateMap<CreateEspecialidadeDto, Especialidade>();
        CreateMap<UpdateEspecialidadeDto, Especialidade>();
        
        // Mapeamentos de Consulta
        CreateMap<Consulta, ConsultaDto>()
            .ForMember(dest => dest.StatusDescricao, opt => opt.MapFrom(src => 
                StatusConsultaExtensions.ToString(StatusConsultaExtensions.FromChar(src.Status))))
            .ForMember(dest => dest.PacienteNome, opt => opt.MapFrom(src => src.Paciente.Nome))
            .ForMember(dest => dest.MedicoNome, opt => opt.MapFrom(src => src.Medico.Nome))
            .ForMember(dest => dest.MedicoCrm, opt => opt.MapFrom(src => src.Medico.Crm))
            .ForMember(dest => dest.EspecialidadeNome, opt => opt.MapFrom(src => src.Especialidade.Nome));
        
        CreateMap<Consulta, ConsultaResumoDto>()
            .ForMember(dest => dest.StatusDescricao, opt => opt.MapFrom(src => 
                StatusConsultaExtensions.ToString(StatusConsultaExtensions.FromChar(src.Status))))
            .ForMember(dest => dest.PacienteNome, opt => opt.MapFrom(src => src.Paciente.Nome))
            .ForMember(dest => dest.MedicoNome, opt => opt.MapFrom(src => src.Medico.Nome))
            .ForMember(dest => dest.EspecialidadeNome, opt => opt.MapFrom(src => src.Especialidade.Nome));
        
        CreateMap<CreateConsultaDto, Consulta>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => 'A')); // Sempre inicia como Agendada
        
        CreateMap<UpdateConsultaDto, Consulta>();
    }
}

