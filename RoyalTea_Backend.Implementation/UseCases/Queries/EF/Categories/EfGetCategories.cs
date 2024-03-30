using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RoyalTea_Backend.Application.UseCases.Commands.Categories;
using RoyalTea_Backend.Application.UseCases.DTO;
using RoyalTea_Backend.Application.UseCases.DTO.Categories;
using RoyalTea_Backend.Application.UseCases.DTO.Searches;
using RoyalTea_Backend.Application.UseCases.DTO.Specifications;
using RoyalTea_Backend.Application.UseCases.Queries.Categories;
using RoyalTea_Backend.DataAccess;
using RoyalTea_Backend.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.UseCases.Queries.EF.Categories
{
    public class EfGetCategories : EfUseCase, IGetCategories
    {
        public EfGetCategories(AppDbContext dbContext)
            : base(dbContext)
        {

        }
        public int Id => 18;

        public string Name => "Get Categories";

        public string Description => "Search categories and view their attached specifications";

        public PagedResponse<CategoryDto> Execute(PagedSearch request)
        {
            var keywords = request.Keywords;

            var query = this.DbContext.Categories.Include(x => x.CategorySpecifications).ThenInclude(x => x.Specification).ThenInclude(x => x.SpecificationValues).Where(x => x.IsActive).AsQueryable();

            if (!String.IsNullOrWhiteSpace(keywords))
                query = query.Where(x => x.Name.ToLower().Contains(keywords.ToLower()));

            var count = query.Count();

            var queryResponse = query.Skip((request.PageNo - 1) * request.PerPage).Take(request.PerPage).ToList();

            var categories = queryResponse.Select(x =>
            {

                var specifications = x.CategorySpecifications.Select(s => new {
                    Id = s.Specification.Id,
                    Name = s.Specification.Name,
                    Values = this.DbContext.SpecificationValues.Where(v => v.SpecificationId == s.Specification.Id).Select(v => new { Id = v.Id, Value = v.Value})
                }).ToList<object>();

                var category = Mapper.Map<CategoryDto>(x);
                category.Specifications = specifications;
                return category;

            });


            var response = new PagedResponse<CategoryDto>(request, count);
            response.Items = categories;

            return response;


        }
    }
}
