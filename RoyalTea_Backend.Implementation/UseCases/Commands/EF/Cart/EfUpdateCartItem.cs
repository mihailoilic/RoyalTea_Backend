using FluentValidation;
using RoyalTea_Backend.Application.Exceptions;
using RoyalTea_Backend.Application.UseCases.Commands.Cart;
using RoyalTea_Backend.Application.UseCases.DTO;
using RoyalTea_Backend.DataAccess;
using RoyalTea_Backend.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.UseCases.Commands.EF.Cart
{
    public class EfUpdateCartItem : EfUseCase, IUpdateCartItem
    {
        public CartValidator validator { get; set; }
        public EfUpdateCartItem(AppDbContext dbContext, CartValidator validator)
            : base(dbContext)
        {
            this.validator = validator;
        }
        public int Id => 9;

        public string Name => "Update Cart Item";

        public string Description => "Users can edit their cart item quantity";

        public void Execute(UpdateCartItemDto request)
        {
            this.validator.ValidateAndThrow(request);

            var cartItem = this.DbContext.CartItems.FirstOrDefault(x => x.UserId == this.DbContext.AppUser.Id && x.ProductId == request.ProductId);
            if (cartItem == null)
                throw new EntityNotFoundException();

            cartItem.Quantity = request.Quantity;
            this.DbContext.SaveChanges();
        }
    }
}
