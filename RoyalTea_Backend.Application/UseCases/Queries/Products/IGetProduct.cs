using RoyalTea_Backend.Application.UseCases.DTO.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Application.UseCases.Queries.Products
{
    public interface IGetProduct : IQuery<int, ProductDto>
    {
    }
}
