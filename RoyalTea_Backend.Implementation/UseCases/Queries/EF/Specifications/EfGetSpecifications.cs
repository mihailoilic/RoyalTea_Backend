using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RoyalTea_Backend.Application.UseCases.DTO;
using RoyalTea_Backend.Application.UseCases.DTO.Searches;
using RoyalTea_Backend.Application.UseCases.DTO.Specifications;
using RoyalTea_Backend.Application.UseCases.Queries.Specifications;
using RoyalTea_Backend.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.UseCases.Queries.EF.Specifications
{
    public class EfGetSpecifications : EfUseCase, IGetSpecifications
    {
        public EfGetSpecifications(AppDbContext dbContext)
            : base(dbContext)
        {

        }
        public int Id => 15;

        public string Name => "Get Specifications";

        public string Description => "Search all specifications and see values";

        public PagedResponse<SpecificationDto> Execute(PagedSearch request)
        {
            var keywords = request.Keywords;

            var query = this.DbContext.Specifications.Include(x => x.SpecificationValues).Where(x => x.IsActive).AsQueryable();

            if (!String.IsNullOrWhiteSpace(keywords))
                query = query.Where(x => x.Name.ToLower().Contains(keywords.ToLower()));

            var count = query.Count();

            var queryResponse = query.Skip((request.PageNo - 1) * request.PerPage).Take(request.PerPage).ToList();

            var specifications = queryResponse.Select(x =>
            {
                var values = x.SpecificationValues.Select(v => Mapper.Map<SpecificationValueDto>(v)).ToList();
                var specification = Mapper.Map<SpecificationDto>(x);
                specification.Values = values;
                return specification;
            });

            var response = new PagedResponse<SpecificationDto>(request, count);
            response.Items = specifications;

            return response;
        }
    }
}
