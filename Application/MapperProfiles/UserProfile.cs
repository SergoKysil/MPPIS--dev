using AutoMapper;
using Application.Dto;
using Entities = Domain.RDBMS.Entities;

namespace Application.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<LoginDto, Entities.User>().ReverseMap();

            CreateMap<UserDto, Entities.User>().ReverseMap()
                .ForMember(a => a.Id, opt => opt.Condition(a => a.Id != 0))
                .ForMember(dto => dto.Location, opt => opt.MapFrom(x => x.Location))
                .ForMember(dto => dto.Role, opt => opt.MapFrom(x => x.Role));

            CreateMap<Entities.User, UserDto>().ReverseMap();

            CreateMap<AddUserDto, Entities.User>().ReverseMap();

            CreateMap<UserProfileDto, Entities.User>().ForMember(x => x.Products, opt => opt.MapFrom(x => x.Products));


        }
    }
}
