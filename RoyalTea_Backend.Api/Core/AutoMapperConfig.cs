using AutoMapper;
using Newtonsoft.Json;
using RoyalTea_Backend.Application.Logging;
using RoyalTea_Backend.Application.UseCases.DTO;
using RoyalTea_Backend.DataAccess;
using RoyalTea_Backend.Domain;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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

                cfg.CreateMap<RegisterDto, User>()
                    .ForMember(dest => dest.UseCases, opt => opt.MapFrom(src => new List<UseCase>()));

                cfg.CreateMap<UseCaseLog, AuditLog>();
                cfg.CreateMap<AddAddressDto, Address>();
                cfg.CreateMap<Address, AddressDto>()
                    .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name));



                cfg.CreateMap<Country, CountryDto>();


                cfg.CreateMissingTypeMaps = true;

            });
        }
    }
}
