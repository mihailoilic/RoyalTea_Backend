using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RoyalTea_Backend.Application.UseCases.DTO;
using RoyalTea_Backend.Application.UseCases.DTO.Products;
using RoyalTea_Backend.Application.UseCases.DTO.Searches;
using RoyalTea_Backend.Application.UseCases.DTO.Specifications;
using RoyalTea_Backend.Application.UseCases.Queries.Products;
using RoyalTea_Backend.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.UseCases.Queries.EF.Products
{
    public class EfGetProducts : EfUseCase, IGetProducts
    {
        public EfGetProducts(AppDbContext dbContext)
            : base(dbContext)
        {

        }

        public int Id => 25;

        public string Name => "Get Products";

        public string Description => "Search and filter products";

        public PagedResponse<ProductDto> Execute(PagedSearch request)
        {
            var keywords = request.Keywords;

            var query = this.DbContext.Products.Include(x => x.Image)
                .Include(x => x.Category).ThenInclude(x => x.CategorySpecifications).ThenInclude(x => x.Specification)
                .Include(x => x.ProductSpecificationValues)
                .Include(x => x.Prices).ThenInclude(x => x.Currency).Where(x => x.IsActive).AsQueryable();

            if (!String.IsNullOrWhiteSpace(keywords))
                query = query.Where(x => x.Title.ToLower().Contains(keywords.ToLower()) || x.Description.ToLower().Contains(keywords.ToLower()));

            var count = query.Count();

            var queryResponse = query.Skip((request.PageNo - 1) * request.PerPage).Take(request.PerPage).ToList();
            var products = queryResponse.Select(x =>
            {
                var product = Mapper.Map<ProductDto>(x);
                product.Prices = x.Prices.Select(p => Mapper.Map<PriceDto>(p)).ToList();
                product.ProductSpecifications = x.Category.CategorySpecifications.Select(cs =>
                {
                    var specification = Mapper.Map<SpecificationDto>(cs.Specification);
                    specification.Values = cs.Specification.SpecificationValues.Where(sv => x.ProductSpecificationValues.Select(psv => psv.SpecificationValueId).Contains(sv.Id))
                        .Select(sv => Mapper.Map<SpecificationValueDto>(sv)).ToList();
                    return specification;
                }).ToList();
                return product;
            });


            var response = new PagedResponse<ProductDto>(request, count);
            response.Items = products;


            return response;
        }
    }
}
