using Microsoft.Extensions.DependencyInjection;
using MPPIS.Services.Implementation;
using Application.Services.Interfaces;
using Application.Services.Implementation;
using AutoMapper;

namespace Application.Services
{
    public static class ServiceExtensios
    {
        public static void AddCistomServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserResolverService, UserResolverService>();
        }
        
        public static void AddMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfiles.UserProfile());
                mc.AddProfile(new MapperProfiles.StorageDataProfile());
                mc.AddProfile(new MapperProfiles.DayPriceProfile());
                mc.AddProfile(new MapperProfiles.RouteDayProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper); 
        }
    }
}
