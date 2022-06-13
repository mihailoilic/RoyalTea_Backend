using FluentValidation;
using RoyalTea_Backend.Application.UseCases.DTO;
using RoyalTea_Backend.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.Validators
{
    public class CartValidator : AbstractValidator<BaseCartItemDto>
    {
        public CartValidator(AppDbContext dbContext)
        {
            RuleFor(x => x.ProductId).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Product Id is required.")
                .Must(x => dbContext.Products.Any(p => p.IsActive && p.Id == x)).WithMessage("Product Id doesn't exist.");
            RuleFor(x => x.Quantity).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Quantity is required.")
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.")
                .LessThan(100).WithMessage("Quantity must be less than 100.");

        }
    }
}
