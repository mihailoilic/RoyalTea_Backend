using FluentValidation;
using RoyalTea_Backend.Application.UseCases.DTO.Currency;
using RoyalTea_Backend.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.Validators
{
    public class CurrencyValidator : AbstractValidator<CurrencyDto>
    {
        public CurrencyValidator(AppDbContext dbContext)
        {
            RuleFor(x => x.IsoCode).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Iso Code is required.")
                .Must(x => !dbContext.Currencies.Any(c => c.IsoCode == x && c.IsActive)).WithMessage("Iso Code must be unique.");
        }
    }
}
