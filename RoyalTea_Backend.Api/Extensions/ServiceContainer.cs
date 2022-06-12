using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RoyalTea_Backend.Api.Core;
using RoyalTea_Backend.Application.UseCases.Commands;
using RoyalTea_Backend.Application.UseCases.Commands.Addresses;
using RoyalTea_Backend.Application.UseCases.DTO;
using RoyalTea_Backend.Application.UseCases.Queries;
using RoyalTea_Backend.Application.UseCases.Queries.Addresses;
using RoyalTea_Backend.DataAccess;
using RoyalTea_Backend.Domain;
using RoyalTea_Backend.Implementation.UseCases.Commands;
using RoyalTea_Backend.Implementation.UseCases.Commands.EF;
using RoyalTea_Backend.Implementation.UseCases.Commands.EF.Addresses;
using RoyalTea_Backend.Implementation.UseCases.Queries.EF;
using RoyalTea_Backend.Implementation.UseCases.Queries.EF.Addresses;
using RoyalTea_Backend.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Api.Extensions
{
    public static class ServiceContainer
    {
        public static void AddAppUser(this IServiceCollection services)
        {
            services.AddTransient<IAppUser>(s =>
            {
                var httpContextAccessor = s.GetService<IHttpContextAccessor>();

                var claims = httpContextAccessor.HttpContext.User;
                if(claims == null || claims.FindFirst("UserId") == null)
                {
                    return new AnonymousAppUser();
                }

                return Mapper.Map<JwtAppUser>(claims);

            });
        }
        public static void AddAppDbContext(this IServiceCollection services)
        {
            services.AddTransient(s =>
            {
                var dbContextOptionsBuilder = new DbContextOptionsBuilder();

                var connString = s.GetService<AppConfiguration>().ConnectionString;
                dbContextOptionsBuilder.UseSqlServer(connString).UseLazyLoadingProxies();

                return new AppDbContext(dbContextOptionsBuilder.Options, s.GetService<IAppUser>());
            });
        }
        public static void AddJwt(this IServiceCollection services, JwtConfiguration jwtConfig)
        {
            services.AddTransient(x =>
            {
                var context = x.GetService<AppDbContext>();
                var config = x.GetService<AppConfiguration>();

                return new JwtManager(context, jwtConfig);
            });


            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtConfig.Issuer,
                    ValidateIssuer = true,
                    ValidAudience = "Any",
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.PrivateKey)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<ISeed, EfSeed>();
            services.AddTransient<IGetUsersQuery, EfGetUsersQuery>();
            services.AddTransient<IRegisterUser, EfRegisterUser>();
            services.AddTransient<ICreateAddress, EfCreateAddress>();
            services.AddTransient<IUpdateAddress, EfUpdateAddress>();
            services.AddTransient<IDeleteAddress, EfDeleteAddress>();
            services.AddTransient<IGetAddresses, EfGetAddresses>();

            services.AddTransient<RegisterUserValidator>();
            services.AddTransient<AddressValidator>();
        }
    }
}
