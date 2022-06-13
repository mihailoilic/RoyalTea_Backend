using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RoyalTea_Backend.Application.Exceptions;
using RoyalTea_Backend.Application.UseCases.DTO.Products;
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
    public class EfGetProduct : EfUseCase, IGetProduct
    {
        public EfGetProduct(AppDbContext dbContext)
            : base(dbContext)
        {

        }
        public int Id => 26;

        public string Name => "Get Product";

        public string Description => "Find single product by id and get details";

        public ProductDto Execute(int request)
        {
            var product = this.DbContext.Products.Include(x => x.Image)
                .Include(x => x.Category).ThenInclude(x => x.CategorySpecifications).ThenInclude(x => x.Specification)
                .Include(x => x.ProductSpecificationValues)
                .Include(x => x.Prices).ThenInclude(x => x.Currency).FirstOrDefault(x => x.IsActive && x.Id == request);

            if (product == null)
                throw new EntityNotFoundException();

            var productDto = Mapper.Map<ProductDto>(product);
            productDto.Prices = product.Prices.Select(p => Mapper.Map<PriceDto>(p)).ToList();
            productDto.ProductSpecifications = product.Category.CategorySpecifications.Select(cs =>
            {
                var specification = Mapper.Map<SpecificationDto>(cs.Specification);
                specification.Values = cs.Specification.SpecificationValues.Where(sv => product.ProductSpecificationValues.Select(psv => psv.SpecificationValueId).Contains(sv.Id))
                    .Select(sv => Mapper.Map<SpecificationValueDto>(sv)).ToList();
                return specification;
            }).ToList();

            return productDto;
         
        }
    }
}
