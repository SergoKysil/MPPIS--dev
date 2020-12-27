using AutoMapper;
using Entities = Domain.RDBMS.Entities;
using Application.Dto;

namespace Application.MapperProfiles
{
    public class RouteDayProfile : Profile
    {
        public RouteDayProfile()
        {
            CreateMap<RouteDayDto, Entities.RouteDay>().ReverseMap();
        }
    }
}
