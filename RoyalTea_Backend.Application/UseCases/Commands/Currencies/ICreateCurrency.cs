using RoyalTea_Backend.Application.UseCases.DTO.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Application.UseCases.Commands.Currencies
{
    public interface ICreateCurrency : ICommand<CurrencyDto>
    {
    }
}
