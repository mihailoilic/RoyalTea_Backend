using AutoMapper;
using RoyalTea_Backend.Application.UseCases.DTO;
using RoyalTea_Backend.Application.UseCases.DTO.Currency;
using RoyalTea_Backend.Application.UseCases.DTO.Searches;
using RoyalTea_Backend.Application.UseCases.Queries.Currencies;
using RoyalTea_Backend.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.UseCases.Queries.EF.Currencies
{
    public class EfGetCurrencies : EfUseCase, IGetCurrencies
    {
        public EfGetCurrencies(AppDbContext dbContext)
            : base(dbContext)
        {

        }
        public int Id => 22;

        public string Name => "Get Currencies";

        public string Description => "Get all Currencies";

        public PagedResponse<CurrencyDto> Execute(PagedSearch request)
        {
            var keywords = request.Keywords;

            var query = this.DbContext.Currencies.Where(x => x.IsActive).AsQueryable();

            if (!String.IsNullOrWhiteSpace(keywords))
                query = query.Where(x => x.IsoCode.ToLower().Contains(keywords.ToLower()));

            var count = query.Count();

            var response = new PagedResponse<CurrencyDto>(request, count);
            response.Items = query.Select(x => Mapper.Map<CurrencyDto>(x)).Skip((request.PageNo - 1) * request.PerPage).Take(request.PerPage).ToList();

            return response;
        }
    }
}
