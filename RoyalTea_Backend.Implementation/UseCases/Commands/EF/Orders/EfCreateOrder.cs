using AutoMapper;
using FluentValidation;
using RoyalTea_Backend.Application.UseCases.Commands.Orders;
using RoyalTea_Backend.Application.UseCases.DTO.Order;
using RoyalTea_Backend.DataAccess;
using RoyalTea_Backend.Domain;
using RoyalTea_Backend.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.UseCases.Commands.EF.Orders
{
    public class EfCreateOrder : EfUseCase, ICreateOrder
    {
        public OrderValidator validator { get; set; }
        public EfCreateOrder(AppDbContext dbContext, OrderValidator validator)
            : base(dbContext)
        {
            this.validator = validator;
        }
        public int Id => 11;

        public string Name => "Create Order";

        public string Description => "Users can place orders";

        public void Execute(CreateOrderDto request)
        {
            this.validator.ValidateAndThrow(request);

            var currency = this.DbContext.Currencies.FirstOrDefault(x => x.Id == request.CurrencyId);

            var orderItems = new List<OrderItem>();
            foreach(var x in request.Items)
            {
                var orderItem = Mapper.Map<OrderItem>(x);
                var product = this.DbContext.Products.FirstOrDefault(p => p.Id == x.ProductId);
                orderItem.Product = product;
                var price = product.Prices.FirstOrDefault(x => x.CurrencyId == currency.Id);
                orderItem.UnitPrice = price != null ? price.Value : 0;
                orderItems.Add(orderItem);
            }
            var order = Mapper.Map<Order>(request);
            order.UserId = this.DbContext.AppUser.Id;
            order.OrderStatus = this.DbContext.OrderStatuses.FirstOrDefault(x => x.Name == "Pending");
            order.OrderItems = orderItems;

            var cartItems = this.DbContext.CartItems.Where(x => x.UserId == this.DbContext.AppUser.Id);
            this.DbContext.CartItems.RemoveRange(cartItems);
            this.DbContext.Orders.Add(order);
            this.DbContext.SaveChanges();

        }
    }
}
