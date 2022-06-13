using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RoyalTea_Backend.Api.Core;
using RoyalTea_Backend.Application.UseCases.Commands;
using RoyalTea_Backend.Application.UseCases.Commands.Addresses;
using RoyalTea_Backend.Application.UseCases.Commands.Cart;
using RoyalTea_Backend.Application.UseCases.Commands.Categories;
using RoyalTea_Backend.Application.UseCases.Commands.Currencies;
using RoyalTea_Backend.Application.UseCases.Commands.Orders;
using RoyalTea_Backend.Application.UseCases.Commands.Products;
using RoyalTea_Backend.Application.UseCases.Commands.Specifications;
using RoyalTea_Backend.Application.UseCases.Commands.Users;
using RoyalTea_Backend.Application.UseCases.Queries;
using RoyalTea_Backend.Application.UseCases.Queries.Addresses;
using RoyalTea_Backend.Application.UseCases.Queries.Cart;
using RoyalTea_Backend.Application.UseCases.Queries.Categories;
using RoyalTea_Backend.Application.UseCases.Queries.Currencies;
using RoyalTea_Backend.Application.UseCases.Queries.Orders;
using RoyalTea_Backend.Application.UseCases.Queries.Products;
using RoyalTea_Backend.Application.UseCases.Queries.Specifications;
using RoyalTea_Backend.DataAccess;
using RoyalTea_Backend.Domain;
using RoyalTea_Backend.Implementation.UseCases.Commands;
using RoyalTea_Backend.Implementation.UseCases.Commands.EF;
using RoyalTea_Backend.Implementation.UseCases.Commands.EF.Addresses;
using RoyalTea_Backend.Implementation.UseCases.Commands.EF.Cart;
using RoyalTea_Backend.Implementation.UseCases.Commands.EF.Categories;
using RoyalTea_Backend.Implementation.UseCases.Commands.EF.Currencies;
using RoyalTea_Backend.Implementation.UseCases.Commands.EF.Orders;
using RoyalTea_Backend.Implementation.UseCases.Commands.EF.Products;
using RoyalTea_Backend.Implementation.UseCases.Commands.EF.Specifications;
using RoyalTea_Backend.Implementation.UseCases.Commands.EF.Users;
using RoyalTea_Backend.Implementation.UseCases.Queries.EF;
using RoyalTea_Backend.Implementation.UseCases.Queries.EF.Addresses;
using RoyalTea_Backend.Implementation.UseCases.Queries.EF.Cart;
using RoyalTea_Backend.Implementation.UseCases.Queries.EF.Categories;
using RoyalTea_Backend.Implementation.UseCases.Queries.EF.Currencies;
using RoyalTea_Backend.Implementation.UseCases.Queries.EF.Orders;
using RoyalTea_Backend.Implementation.UseCases.Queries.EF.Products;
using RoyalTea_Backend.Implementation.UseCases.Queries.EF.Specifications;
using RoyalTea_Backend.Implementation.Validators;
using System;
using System.Text;

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
            services.AddTransient<IRegisterUser, EfRegisterUser>();

            services.AddTransient<ICreateAddress, EfCreateAddress>();
            services.AddTransient<IUpdateAddress, EfUpdateAddress>();
            services.AddTransient<IDeleteAddress, EfDeleteAddress>();
            services.AddTransient<IGetAddresses, EfGetAddresses>();

            services.AddTransient<IGetCartItems, EfGetCartItems>();
            services.AddTransient<ICreateCartItem, EfCreateCartItem>();
            services.AddTransient<IUpdateCartItem, EfUpdateCartItem>();
            services.AddTransient<IDeleteCartItem, EfDeleteCartItem>();

            services.AddTransient<IGetOrders, EfGetOrders>();
            services.AddTransient<ICreateOrder, EfCreateOrder>();
            services.AddTransient<ICancelOrder, EfCancelOrder>();

            services.AddTransient<IGetSpecifications, EfGetSpecifications>();
            services.AddTransient<ICreateSpecification, EfCreateSpecification>();
            services.AddTransient<IUpdateSpecification, EfUpdateSpecification>();
            services.AddTransient<IDeleteSpecification, EfDeleteSpecification>();

            services.AddTransient<IGetCategories, EfGetCategories>();
            services.AddTransient<ICreateCategory, EfCreateCategory>();
            services.AddTransient<IUpdateCategory, EfUpdateCategory>();
            services.AddTransient<IDeleteCategory, EfDeleteCategory>();

            services.AddTransient<IGetCurrencies, EfGetCurrencies>();
            services.AddTransient<ICreateCurrency, EfCreateCurrency>();
            services.AddTransient<IDeleteCurrency, EfDeleteCurrency>();

            services.AddTransient<IGetProducts, EfGetProducts>();
            services.AddTransient<IGetProduct, EfGetProduct>();
            services.AddTransient<ICreateProduct, EfCreateProduct>();
            services.AddTransient<IUpdateProduct, EfUpdateProduct>();
            services.AddTransient<IDeleteProduct, EfDeleteProduct>();

            services.AddTransient<IGetUsersQuery, EfGetUsersQuery>();
            services.AddTransient<IUpdateUser, EfUpdateUser>();
            //services.AddTransient<IDeleteUser, EfDeleteUser>();


            services.AddTransient<RegisterUserValidator>();
            services.AddTransient<AddressValidator>();
            services.AddTransient<CartValidator>();
            services.AddTransient<OrderValidator>();
            services.AddTransient<SpecificationValidator>();
            services.AddTransient<CategoryValidator>();
            services.AddTransient<CurrencyValidator>();
            services.AddTransient<ProductValidator>();
            services.AddTransient<UserValidator>();

            
        }
    }
}
