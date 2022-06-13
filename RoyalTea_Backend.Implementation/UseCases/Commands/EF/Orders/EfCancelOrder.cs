using Microsoft.EntityFrameworkCore;
using RoyalTea_Backend.Application.Exceptions;
using RoyalTea_Backend.Application.UseCases.Commands.Orders;
using RoyalTea_Backend.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.UseCases.Commands.EF.Orders
{
    public class EfCancelOrder : EfUseCase, ICancelOrder
    {
        public EfCancelOrder(AppDbContext dbContext)
            : base(dbContext)
        {

        }
        public int Id => 13;

        public string Name => "Cancel Order";

        public string Description => "Users can cancel their orders according to order status";

        public void Execute(int request)
        {
            var order = this.DbContext.Orders.Include(x => x.OrderStatus).FirstOrDefault(x => x.Id == request && x.UserId == this.DbContext.AppUser.Id);
            if (order == null)
                throw new EntityNotFoundException();
            if (!order.OrderStatus.IsCancellable)
                throw new UseCaseConflictException("The order cannot be cancelled.");

            order.IsCancelled = true;
            order.OrderStatus = this.DbContext.OrderStatuses.FirstOrDefault(x => x.Name == "Cancelled");

            this.DbContext.SaveChanges();
        }
    }
}
