using AutoMapper;
using Practica_API.Modelos;
using Practica_API.Modelos.Dto;

namespace Practica_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Practica, PracticaDto>();
            CreateMap<PracticaDto, Practica>();

            CreateMap<Practica, PracticaCreateDto>().ReverseMap();
            CreateMap<Practica, PracticaUpdateDto>().ReverseMap();


        
        }
    }
}
