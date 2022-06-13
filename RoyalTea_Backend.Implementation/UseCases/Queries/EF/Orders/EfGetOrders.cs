using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RoyalTea_Backend.Application.UseCases.DTO;
using RoyalTea_Backend.Application.UseCases.DTO.Order;
using RoyalTea_Backend.Application.UseCases.DTO.Searches;
using RoyalTea_Backend.Application.UseCases.Queries.Orders;
using RoyalTea_Backend.DataAccess;
using RoyalTea_Backend.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.UseCases.Queries.EF.Orders
{
    public class EfGetOrders : EfUseCase, IGetOrders
    {
        public EfGetOrders(AppDbContext dbContext)
            : base(dbContext)
        {

        }

        public int Id => 12;

        public string Name => "Get Orders";

        public string Description => "User can see all orders they've placed";
        public PagedResponse<OrderDto> Execute(PagedOrderSearch request)
        {

            var keywords = request.Keywords;

            var query = this.DbContext.Orders.Include(x => x.OrderStatus).Include(x => x.OrderItems).Where(x => x.UserId == this.DbContext.AppUser.Id).AsQueryable();

            if (!String.IsNullOrWhiteSpace(keywords))
                query = query.Where(x => x.OrderItems.Any(i => i.Product.Title.ToLower().Contains(keywords.ToLower())));

            if (request.DateFrom != null)
                query = query.Where(x => x.CreatedAt > request.DateFrom);

            if(request.DateTo != null)
                query = query.Where(x => x.CreatedAt < request.DateTo);

            var count = query.Count();

            var queryResponse = query.Skip((request.PageNo - 1) * request.PerPage).Take(request.PerPage).ToList();

            var orders = new List<OrderDto>();

            foreach(var order in queryResponse)
            {
                var orderDto = Mapper.Map<OrderDto>(order);
                orderDto.Items = order.OrderItems.Select(x => Mapper.Map<OrderItemDto>(x)).ToList();
                orderDto.Status = Mapper.Map<OrderStatusDto>(order.OrderStatus);
                orders.Add(orderDto);
            }

            var response = new PagedResponse<OrderDto>(request, count);
            response.Items = orders;

            return response;
        }
    }
}
