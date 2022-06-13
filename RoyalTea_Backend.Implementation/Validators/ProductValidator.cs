using FluentValidation;
using RoyalTea_Backend.Application.UseCases.DTO.Products;
using RoyalTea_Backend.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.Validators
{
    public class ProductValidator : AbstractValidator<CreateProductDto>
    {
        public ProductValidator(AppDbContext dbContext)
        {
            RuleFor(x => x).Cascade(CascadeMode.Stop)
                .Must(x => !(x is UpdateProductDto) ? !dbContext.Products.Any(p => p.Title == x.Title) : true).WithMessage("Product with this title already exists");

            RuleFor(x => x.Title).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(50).WithMessage("Maximum length: 50")
                .MinimumLength(5).WithMessage("Minimum length: 3");

            RuleFor(x => x.Image).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Image is required")
                .MaximumLength(100).WithMessage("Maximum Length: 100")
                .MinimumLength(3).WithMessage("Minimum Length: 3");

            RuleFor(x => x.Description).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Description is required.")
                .MinimumLength(20).WithMessage("Minimum length: 20");

            RuleFor(x => x.CategoryId).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Category Id is required.")
                .Must(x => dbContext.Categories.Any(c => c.IsActive && c.Id == x)).WithMessage("Category Id {PropertyValue} doesn't exist");

            RuleFor(x => x.SpecificationValueIds).Cascade(CascadeMode.Stop)
                .ForEach(x => x.Must(id => dbContext.SpecificationValues.Any(sv => sv.Id == id)).WithMessage("Specification Value doesn't exist."));

            RuleFor(x => x.Prices).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Prices are required")
                .ForEach(x =>
                {
                    x.Must(price => dbContext.Currencies.Any(p => p.IsActive && p.IsoCode == price.CurrencyIso)).WithMessage("Iso code doesn't exist.");
                    x.Must(price => price.Value > 0).WithMessage("Price value must be greater than 0");
                });

        }
    }
}
