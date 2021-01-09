using Microsoft.Extensions.DependencyInjection;
using Application.Services.Interfaces;
using Application.Services.Implementation;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Domain.RDBMS.Entities;
using Application.Dto;

namespace Application.Services
{
    public static class ServiceExtensios
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
           
            services.AddScoped<IUserService, UserService>();
           
            services.AddScoped<IUserResolverService, UserResolverService>();

            services.AddScoped<ILocationService, LocationService>();
        }
        
        public static void AddMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfiles.UserProfile());
                mc.AddProfile(new MapperProfiles.StorageDataProfile());
                mc.AddProfile(new MapperProfiles.DayPriceProfile());
                mc.AddProfile(new MapperProfiles.RouteDayProfile());
                mc.AddProfile(new MapperProfiles.LocationProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper); 
        }
    }
}
