using RoyalTea_Backend.Application.UseCases.DTO.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Application.UseCases.Commands.Products
{
    public interface ICreateProduct : ICommand<CreateProductDto>
    {
    }
}
