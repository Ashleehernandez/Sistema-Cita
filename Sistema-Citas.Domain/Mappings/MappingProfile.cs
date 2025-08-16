

using AutoMapper;
using Sistema_Citas.Domain.DTo.CitasDto;
using Sistema_Citas.Domain.DTo.EstacionDto;
using Sistema_Citas.Domain.DTo.UsuarioDto;
using Sistema_Citas.Domain.Entity;


namespace Sistema_Citas.Domain.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ✅ MAPEOS BIDIRECCIONALES PARA USUARIOS
            // De DTO a Entity (para crear/actualizar)
            CreateMap<CreateUsuarioDto, Usuario>();
            CreateMap<UpdateUsuarioDto, Usuario>();
            CreateMap<DeleteUsuarioDto, Usuario>();

            // De Entity a DTO (para respuestas)
            CreateMap<Usuario, GetUsuarioDto>();
            CreateMap<Usuario, CreateUsuarioDto>();
            CreateMap<Usuario, UpdateUsuarioDto>();
            CreateMap<Usuario, DeleteUsuarioDto>();

            // ✅ MAPEOS BIDIRECCIONALES PARA CITAS
            // De DTO a Entity (para crear/actualizar)
            CreateMap<CreateCitaDto, Cita>()
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => Domain.Enum.Estado.Pendiente)) // Estado por defecto
                .ForMember(dest => dest.CitaId, opt => opt.Ignore()); // Ignorar ID en creación
            CreateMap<UpdateCitasDto, Cita>();
            CreateMap<DeleteCitasDto, Cita>();

            // De Entity a DTO (para respuestas)
            CreateMap<Cita, GetCitasDto>();
            CreateMap<Cita, CreateCitaDto>();
            CreateMap<Cita, UpdateCitasDto>();
            CreateMap<Cita, DeleteCitasDto>();

            // ✅ MAPEOS BIDIRECCIONALES PARA ESTACIONES
            // De DTO a Entity (para crear/actualizar)
            CreateMap<CreateEstacionDto, Estacion>()
                .ForMember(dest => dest.EstacionId, opt => opt.Ignore()); // Ignorar ID en creación
            CreateMap<UpdateEstacionDto, Estacion>()
                 .ForMember(dest => dest.EstacionId, opt => opt.Ignore()); 
            CreateMap<DeleteEstacionDto, Estacion>();

            // De Entity a DTO (para respuestas)
            CreateMap<Estacion, GetEstacionDto>();
            CreateMap<Estacion, CreateEstacionDto>();
            CreateMap<Estacion, UpdateEstacionDto>();
            CreateMap<Estacion, DeleteEstacionDto>();

        }
    }
}
