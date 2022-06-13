using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RoyalTea_Backend.Application.UseCases;
using RoyalTea_Backend.Application.UseCases.DTO;
using RoyalTea_Backend.Application.UseCases.DTO.Searches;
using RoyalTea_Backend.Application.UseCases.Queries.Addresses;
using RoyalTea_Backend.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.UseCases.Queries.EF.Addresses
{
    public class EfGetAddresses : EfUseCase, IGetAddresses
    {
        public EfGetAddresses(AppDbContext dbContext)
            : base(dbContext)
        {

        }

        public int Id => 6;

        public string Name => "Get Addresses";

        public string Description => "User can view and search all their addresses";

        public PagedResponse<AddressDto> Execute(PagedSearch request)
        {
            var keywords = request.Keywords;

            var query = this.DbContext.Addresses.Include(x => x.Country).Where(x => x.UserId == this.DbContext.AppUser.Id && x.IsActive).AsQueryable();

            if (!String.IsNullOrWhiteSpace(keywords))
                query = query.Where(x => x.DeliveryLocation.ToLower().Contains(keywords.ToLower()) || x.Country.Name.ToLower().Contains(keywords.ToLower()));

            var count = query.Count();

            var response = new PagedResponse<AddressDto>(request, count);
            response.Items = query.Select(x => Mapper.Map<AddressDto>(x)).Skip((request.PageNo - 1) * request.PerPage).Take(request.PerPage).ToList();

            return response;
        }
    }
}
