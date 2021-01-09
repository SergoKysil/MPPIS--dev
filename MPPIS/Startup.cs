using Application.Dto;
using Application.Services;
using Domain.RDBMS;
using Domain.RDBMS.Entities;
using Infrastructure;
using Infrastructure.RDBMS;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace MPPIS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration;




        public void ConfigureServices(IServiceCollection services)
        {
            Configuration.Bind("Project", new Config());

            services.AddDbContext<AppDbContext>(x => x.UseSqlServer(Config.ConnectionString, b=> b.MigrationsAssembly("MPPIS")));

            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddCustomServices();

            services.AddMapper();

            services.AddMvc();

            services.AddControllersWithViews();

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();

            app.UseStaticFiles();
            
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=AddNewUser}/{action=AddNewUser}/{id?}");
                endpoints.MapRazorPages();
            }); 

        }
    }
}
