using AutoMapper;
using FluentValidation;
using RoyalTea_Backend.Application.UseCases.Commands.Currencies;
using RoyalTea_Backend.Application.UseCases.DTO.Currency;
using RoyalTea_Backend.DataAccess;
using RoyalTea_Backend.Domain;
using RoyalTea_Backend.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.UseCases.Commands.EF.Currencies
{
    public class EfCreateCurrency : EfUseCase, ICreateCurrency
    {
        public CurrencyValidator validator { get; set; }
        public EfCreateCurrency(AppDbContext dbContext, CurrencyValidator validator)
            : base(dbContext)
        {
            this.validator = validator;
        }
        public int Id => 23;

        public string Name => "Create Currency";

        public string Description => "Create new currency with specified ISO code";

        public void Execute(CurrencyDto request)
        {
            this.validator.ValidateAndThrow(request);

            this.DbContext.Currencies.Add(Mapper.Map<Currency>(request));
            this.DbContext.SaveChanges();

        }
    }
}
