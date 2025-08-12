

using AutoMapper;
using Sistema_Citas.Domain.DTo.CitasDto;
using Sistema_Citas.Domain.DTo.EstacionDto;
using Sistema_Citas.Domain.DTo.UsuarioDto;
using Sistema_Citas.Domain.Entity;
using System.Threading;

namespace Sistema_Citas.Domain.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Mapping For Usuarios and UsuarioDTO
            CreateMap<Usuario, CreateUsuarioDto>();
            CreateMap<Usuario, UpdateUsuarioDto>();
            CreateMap<Usuario, DeleteUsuarioDto>();
            CreateMap<Usuario, GetUsuarioDto>();

            //Mapping For Citas and CitaDTO
            CreateMap<Cita, CreateCitaDto>();
            CreateMap<Cita, UpdateCitasDto>();
            CreateMap<Cita, DeleteCitasDto>();
            CreateMap<Cita, GetCitasDto>();

            //Mapping For Estaciones and EstacionDTO
            CreateMap<Estacion, CreateEstacionDto>();
            CreateMap<Estacion, UpdateEstacionDto>();
            CreateMap<Estacion, DeleteEstacionDto>();
            CreateMap<Estacion, GetEstacionDto>();

        }
    }
}
