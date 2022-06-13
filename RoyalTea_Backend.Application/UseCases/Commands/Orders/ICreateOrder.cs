using RoyalTea_Backend.Application.UseCases.DTO.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Application.UseCases.Commands.Orders
{
    public interface ICreateOrder : ICommand<CreateOrderDto>
    {
    }
}
