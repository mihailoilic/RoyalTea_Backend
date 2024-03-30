using RoyalTea_Backend.Application.UseCases.DTO.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Application.UseCases.DTO
{
    public class BaseCartItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
    public class CartItemDto : BaseCartItemDto
    {
        public int Id { get; set; }
        public ProductDto Product { get; set; }
    }

    public class CreateCartItemDto : BaseCartItemDto
    {

    }

    public class UpdateCartItemDto : BaseCartItemDto
    {

    }


}
