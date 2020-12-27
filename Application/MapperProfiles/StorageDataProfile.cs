using AutoMapper;
using Entities = Domain.RDBMS.Entities;
using Application.Dto;

namespace Application.MapperProfiles
{
    public class StorageDataProfile : Profile
    {
        public StorageDataProfile()
        {
            CreateMap<StorageDataDto, Entities.StorageData>().ReverseMap()
                .ForMember(x => x.DayPrice, opt => opt.MapFrom(x => x.DayPriceId))
                .ForMember(x => x.User, opt => opt.MapFrom(x => x.UserId));

            CreateMap<AddStorageDataDto, Entities.StorageData>().ReverseMap()
                .ForMember(x => x.User, opt => opt.MapFrom(x => x.UserId));

        }
    }
}
