using RoyalTea_Backend.Application.Exceptions;
using RoyalTea_Backend.Application.UseCases.Commands.Cart;
using RoyalTea_Backend.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.UseCases.Commands.EF.Cart
{
    public class EfDeleteCartItem : EfUseCase, IDeleteCartItem
    {
        public EfDeleteCartItem(AppDbContext dbContext)
            : base(dbContext)
        {

        }
        public int Id => 10;

        public string Name => "Delete Cart Item";

        public string Description => "User can delete one of their cart items";

        public void Execute(int request)
        {
            var cartItem = this.DbContext.CartItems.FirstOrDefault(x => x.UserId == this.DbContext.AppUser.Id && x.ProductId == request);
            if (cartItem == null)
                throw new EntityNotFoundException();

            this.DbContext.CartItems.Remove(cartItem);
            this.DbContext.SaveChanges();
        }
    }
}
