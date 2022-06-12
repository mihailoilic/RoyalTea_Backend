using FluentValidation;
using RoyalTea_Backend.Application.UseCases.DTO;
using RoyalTea_Backend.DataAccess;
using RoyalTea_Backend.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.Validators
{
    public class AddressValidator : AbstractValidator<AddAddressDto>
    {
        public AddressValidator(AppDbContext dbContext)
        {
            RuleFor(x => x.DeliveryLocation)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Delivery location is required.")
                .MinimumLength(10).WithMessage("Minimum 10 characters.")
                .MaximumLength(200).WithMessage("Maximum 200 characters.");

            RuleFor(x => x.CountryId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Country Id is required.")
                .Must(x => dbContext.Countries.Any(c => c.Id == x )).WithMessage("Country Id {PropertyValue} wasn't found.");
        }
    }
}
