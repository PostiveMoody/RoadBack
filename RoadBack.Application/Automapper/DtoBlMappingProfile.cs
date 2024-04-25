using AutoMapper;
using RoadBack.Domain.Models;

namespace RoadBack.Application.Automapper
{
    public class DtoBlMappingProfile : Profile
    {
        public DtoBlMappingProfile()
        {
            CreateMap<DTO.Category, Category>();
            CreateMap<Category, DTO.Category>();
        }
    }
}
