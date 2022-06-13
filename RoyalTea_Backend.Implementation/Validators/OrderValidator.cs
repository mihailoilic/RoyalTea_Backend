using FluentValidation;
using RoyalTea_Backend.Application.UseCases.DTO.Order;
using RoyalTea_Backend.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.Validators
{
    public class OrderValidator : AbstractValidator<CreateOrderDto>
    {
        public OrderValidator(AppDbContext dbContext)
        {
            RuleFor(x => x.AddressId).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Address Id is required.")
                .Must(x => dbContext.Addresses.Any(a => a.Id == x && a.UserId == dbContext.AppUser.Id)).WithMessage("Address Id for this user doesn't exist");

            RuleFor(x => x.Items).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Order Items are required.")
                .ForEach(x => x.Must(i => dbContext.Products.Any(p => p.Id == i.ProductId && p.IsActive))).WithMessage("Product Id {PropertyValue} doesn't exist.")
                .ForEach(x => x.Must(i => i.Quantity > 0)).WithMessage("Quantity for all items must be higher than 0")
                .Must(x => x.Count() == x.Select(d => d.ProductId).Distinct().Count()).WithMessage("Product Ids must be distinct");

            RuleFor(x => x.CurrencyId).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Currency Id is required.")
                .Must(x => dbContext.Currencies.Any(c => c.Id == x)).WithMessage("Currency Id {PropertyValue} doesn't exist");

        }
    }
}
