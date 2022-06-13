using AutoMapper;
using FluentValidation;
using RoyalTea_Backend.Application.UseCases.Commands.Cart;
using RoyalTea_Backend.Application.UseCases.DTO;
using RoyalTea_Backend.DataAccess;
using RoyalTea_Backend.Domain;
using RoyalTea_Backend.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.UseCases.Commands.EF.Cart
{
    public class EfCreateCartItem : EfUseCase, ICreateCartItem
    {
        public CartValidator validator { get; set; }
        public EfCreateCartItem(AppDbContext dbContext, CartValidator validator)
            : base(dbContext)
        {
            this.validator = validator;
        }
        public int Id => 7;

        public string Name => "Create Cart Item";

        public string Description => "Users can add products to cart";

        public void Execute(CreateCartItemDto request)
        {
            this.validator.ValidateAndThrow(request);

            var existingCartItem = this.DbContext.CartItems.FirstOrDefault(x => x.ProductId == request.ProductId && x.UserId == this.DbContext.AppUser.Id);
            if(existingCartItem != null)
            {
                existingCartItem.Quantity += request.Quantity;
                this.DbContext.SaveChanges();
                return;
            }

            var cartItem = Mapper.Map<CartItem>(request);
            cartItem.UserId = this.DbContext.AppUser.Id;

            this.DbContext.CartItems.Add(cartItem);
            this.DbContext.SaveChanges();

        }
    }
}
