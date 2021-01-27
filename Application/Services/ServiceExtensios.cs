using Microsoft.Extensions.DependencyInjection;
using Application.Services.Interfaces;
using Application.Services.Implementation;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public static class ServiceExtensios
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
           
            services.AddScoped<IUserService, UserService>();
           
            services.AddScoped<IUserResolverService, UserResolverService>();

            services.AddScoped<ILocationService, LocationService>();

            services.AddScoped<IDayPriceService, DayPriceService>();

            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<ITokenService, TokenService>();
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

       public static void AddJWTAuthenticatoin(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JwtToken:Issuer"],
                    ValidAudience = configuration["JwtToken:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtToken:SecretKey"]))
                };
            });
        }
    }
}
