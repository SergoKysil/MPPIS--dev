using AutoMapper;
using Application.Dto;
using Entities = Domain.RDBMS.Entities;

namespace Application.MapperProfiles
{
    public class DayPriceProfile : Profile
    {
        public DayPriceProfile()
        {
            CreateMap<DayPriceDto, Entities.DayPrice>().ReverseMap()
                .ForMember(x => x.Products, opt => opt.MapFrom(x => x.StorageData));

            CreateMap<AddDayPriceDto, Entities.DayPrice>().ReverseMap();
        }
    }
}
