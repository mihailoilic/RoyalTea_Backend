using RoyalTea_Backend.Application.UseCases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Application.UseCases.Commands.Addresses
{
    public interface ICreateAddress : ICommand<AddAddressDto>
    {
    }
}
