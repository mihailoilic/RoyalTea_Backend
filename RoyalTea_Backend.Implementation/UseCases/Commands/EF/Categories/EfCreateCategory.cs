using AutoMapper;
using FluentValidation;
using RoyalTea_Backend.Application.UseCases.Commands.Categories;
using RoyalTea_Backend.Application.UseCases.DTO.Categories;
using RoyalTea_Backend.DataAccess;
using RoyalTea_Backend.Domain;
using RoyalTea_Backend.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.UseCases.Commands.EF.Categories
{
    public class EfCreateCategory : EfUseCase, ICreateCategory
    {
        public CategoryValidator validator { get; set; }

        public EfCreateCategory(AppDbContext dbContext, CategoryValidator validator)
            : base(dbContext)
        {
            this.validator = validator;
        }
        public int Id => 19;

        public string Name => "Create Category";

        public string Description => "Create Category and attach specifications";

        public void Execute(CreateCategoryDto request)
        {
            this.validator.ValidateAndThrow(request);

            var category = Mapper.Map<Category>(request);
            category.CategorySpecifications = request.SpecificationIds.Select(x => new CategorySpecification { SpecificationId = x }).ToList();

            this.DbContext.Categories.Add(category);
            this.DbContext.SaveChanges();
        }
    }
}
