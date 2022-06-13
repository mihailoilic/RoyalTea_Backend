using RoyalTea_Backend.Application.UseCases.DTO;
using RoyalTea_Backend.Application.UseCases.DTO.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Application.UseCases.Queries.Cart
{
    public interface IGetCartItems : IQuery<PagedCartItemSearch, PagedCartItemResponse>
    {

    }
}
