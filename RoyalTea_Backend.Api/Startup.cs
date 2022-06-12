using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RoyalTea_Backend.Api.Core;
using RoyalTea_Backend.Application;
using RoyalTea_Backend.Application.Logging;
using RoyalTea_Backend.Implementation;
using RoyalTea_Backend.Implementation.Logging;
using RoyalTea_Backend.Api.Extensions;
using ASPNedelja3Vezbe.Api.Core;
using System;
using RoyalTea_Backend.DataAccess;

namespace RoyalTea_Backend.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        [Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            var appConfig = new AppConfiguration();
            Configuration.Bind(appConfig);
            services.AddSingleton(appConfig);

            AutoMapperConfig.InitAutoMapper(appConfig);

            services.AddHttpContextAccessor();

            services.AddJwt(appConfig.JwtConfig);

            services.AddAppUser();
            services.AddAppDbContext();

            services.AddTransient<IUseCaseHandler, AppUseCaseHandler>();
            services.AddTransient<IUseCaseLogger, EfUseCaseLogger>();
            services.AddTransient<IExceptionLogger, ConsoleExceptionLogger>();

            services.AddUseCases();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RoyalTea_Backend.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RoyalTea_Backend.Api v1"));
            }


            app.UseRouting();

            app.UseMiddleware<GlobalExceptionHandler>();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
