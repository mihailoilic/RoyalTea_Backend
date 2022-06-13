using FluentValidation;
using RoyalTea_Backend.Application.UseCases.DTO.Categories;
using RoyalTea_Backend.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.Validators
{
    public class CategoryValidator : AbstractValidator<CreateCategoryDto>
    {
        public CategoryValidator(AppDbContext dbContext)
        {
            RuleFor(x => x).Cascade(CascadeMode.Stop)
                .Must(x => !(x is UpdateCategoryDto) ? !dbContext.Categories.Any(c => c.Name == x.Name) : true).WithMessage("Category Name must be unique");

            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Category name is required.")
                .MinimumLength(3).WithMessage("Minimum length: 3")
                .MaximumLength(40).WithMessage("Maximum length: 40");

            RuleFor(x => x.SpecificationIds).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Specification Ids are required.")
                .ForEach(x => x.Must(s => dbContext.Specifications.Any(sp => sp.Id == s && sp.IsActive))).WithMessage("Specification Id {PropertyValue} doesn't exist.");


        }
    }
}
