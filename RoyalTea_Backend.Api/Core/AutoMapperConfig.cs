using AutoMapper;
using Newtonsoft.Json;
using RoyalTea_Backend.Application.Logging;
using RoyalTea_Backend.Application.UseCases.DTO;
using RoyalTea_Backend.Application.UseCases.DTO.Categories;
using RoyalTea_Backend.Application.UseCases.DTO.Currency;
using RoyalTea_Backend.Application.UseCases.DTO.Order;
using RoyalTea_Backend.Application.UseCases.DTO.Products;
using RoyalTea_Backend.Application.UseCases.DTO.Specifications;
using RoyalTea_Backend.Domain;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace RoyalTea_Backend.Api.Core
{
    public static class AutoMapperConfig
    {
        [Obsolete]
        public static void InitAutoMapper(AppConfiguration appConfig)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ClaimsPrincipal, JwtAppUser>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Convert.ToInt32(src.FindFirst("UserId").Value)))
                    .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.FindFirst("Username").Value))
                    .ForMember(dest => dest.UseCaseIds, opt => opt.MapFrom(src => JsonConvert.DeserializeObject<List<int>>(src.FindFirst("UseCaseIds").Value)));

                cfg.CreateMap<User, List<Claim>>()
                    .ForCtorParam("collection", opt => opt.MapFrom((src, dest) =>
                    {
                        var config = appConfig.JwtConfig;

                        return new List<Claim>
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Iss, config.Issuer),
                            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64, config.Issuer),
                            new Claim("UserId", src.Id.ToString(), ClaimValueTypes.String, config.Issuer),
                            new Claim("Username", src.Username, ClaimValueTypes.String, config.Issuer),
                            new Claim("UseCaseIds", JsonConvert.SerializeObject(src.UseCases.Select(x => x.UseCaseId)), ClaimValueTypes.String, config.Issuer)
                        };
                    }));

                cfg.CreateMap<User, UserDto>()
                    .ForMember(dest => dest.UseCaseIds, opt => opt.MapFrom(src => src.UseCases.Select(x => x.UseCaseId)));
                cfg.CreateMap<UserDto, User>()
                    .ForMember(dest => dest.UseCases, opt => opt.MapFrom(src => src.UseCaseIds.Select(x => new UseCase { UseCaseId = x })));

                cfg.CreateMap<RegisterDto, User>()
                    .ForMember(dest => dest.UseCases, opt => opt.MapFrom(src => new List<UseCase>()));

                cfg.CreateMap<UseCaseLog, AuditLog>();

                cfg.CreateMap<Country, CountryDto>();
                cfg.CreateMap<AddAddressDto, Address>();
                cfg.CreateMap<Address, AddressDto>()
                    .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name));

                cfg.CreateMap<CreateCartItemDto, CartItem>();
                cfg.CreateMap<CartItem, CartItemDto>()
                    .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Title));

                cfg.CreateMap<OrderStatus, OrderStatusDto>();
                cfg.CreateMap<OrderItem, OrderItemDto>()
                    .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Title))
                    .ForMember(dest => dest.Subtotal, opt => opt.MapFrom(src => src.UnitPrice * src.Quantity));
                cfg.CreateMap<Order, OrderDto>()
                    .ForMember(dest => dest.Total, opt => opt.MapFrom(src =>
                        src.OrderItems.Sum(x => x.UnitPrice * x.Quantity)
                    ));
                cfg.CreateMap<CreateOrderDto, Order>();
                cfg.CreateMap<BaseOrderItemDto, OrderItem>();

                cfg.CreateMap<Specification, SpecificationDto>();
                cfg.CreateMap<SpecificationValue, SpecificationValueDto>();
                cfg.CreateMap<CreateSpecificationDto, Specification>();
                cfg.CreateMap<BaseSpecificationValueDto, SpecificationValue>();

                cfg.CreateMap<Category, CategoryDto>();
                cfg.CreateMap<Category, BaseCategoryDto>();
                cfg.CreateMap<CreateCategoryDto, Category>();

                cfg.CreateMap<Product, ProductDto>()
                    .ForMember(d => d.Image, opt => opt.MapFrom(s => s.Image.Path));
                cfg.CreateMap<CreateProductDto, Product>()
                    .ForMember(d => d.CategoryId, opt => opt.MapFrom(s => s.CategoryId))
                    .ForMember(d => d.ProductSpecificationValues, opt => opt.MapFrom(s => s.SpecificationValueIds.Select(x => new ProductSpecificationValue { SpecificationValueId = x }).ToList()));
                    

                cfg.CreateMap<Price, PriceDto>()
                    .ForMember(d => d.CurrencyIso, opt => opt.MapFrom(s => s.Currency.IsoCode));

                cfg.CreateMap<Currency, CurrencyDto>();
                cfg.CreateMap<CurrencyDto, Currency>();


                cfg.CreateMap<AuditLog, AuditLogDto>();


                cfg.ValidateInlineMaps = false;
                cfg.CreateMissingTypeMaps = true;

            });
        }
    }
}
