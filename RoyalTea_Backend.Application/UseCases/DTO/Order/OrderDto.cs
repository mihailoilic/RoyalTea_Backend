using RoyalTea_Backend.Application.UseCases.DTO.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Application.UseCases.DTO.Order
{
    public class BaseOrderDto
    {

        public ICollection<BaseOrderItemDto> Items {get; set;}
        
    }

    public class BaseOrderItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
    public class OrderItemDto : BaseOrderItemDto
    {
        public decimal UnitPrice { get; set; }
        public string ProductName { get; set; }

        public decimal Subtotal { get; set; }

    }

    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public AddressDto Address { get; set; }
        public OrderStatusDto Status { get; set; }
        public CurrencyDto Currency { get; set; }
        public ICollection<OrderItemDto> Items { get; set; }

        public decimal Total { get; set; }
    }

    public class CreateOrderDto : BaseOrderDto
    {
        public int AddressId { get; set; }
        public int CurrencyId { get; set; }

    }

    public class OrderStatusDto
    {
        public string Name { get; set; }
        public bool IsCancellable { get; set; }
    }

    
}
