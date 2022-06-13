using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RoyalTea_Backend.Application.UseCases.DTO;
using RoyalTea_Backend.Application.UseCases.DTO.Searches;
using RoyalTea_Backend.Application.UseCases.Queries.Cart;
using RoyalTea_Backend.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.UseCases.Queries.EF.Cart
{
    public class EfGetCartItems : EfUseCase, IGetCartItems
    {
        public EfGetCartItems(AppDbContext dbContext)
            : base(dbContext)
        {

        }
        public int Id => 8;

        public string Name => "Get Cart Items";

        public string Description => "Users can view their cart items";

        public PagedCartItemResponse Execute(PagedCartItemSearch request)
        {
            var keywords = request.Keywords;

            var query = this.DbContext.CartItems.Include(x => x.Product).ThenInclude(x => x.Prices).ThenInclude(x => x.Currency)
                .Where(x => x.UserId == this.DbContext.AppUser.Id).AsQueryable();

            if (!String.IsNullOrWhiteSpace(keywords))
                query = query.Where(x => x.Product.Title.ToLower().Contains(keywords.ToLower()));

            var count = query.Count();

            

            var response = new PagedCartItemResponse(request, count);
            response.Items = query.Skip((request.PageNo - 1) * request.PerPage).Take(request.PerPage).ToList()
                .Select(x =>
                {
                    var cartItemDto = Mapper.Map<CartItemDto>(x);
                    var unitPriceObj = x.Product.Prices.FirstOrDefault(p => p.CurrencyId == request.CurrencyId);
                    cartItemDto.UnitPrice = unitPriceObj != null ? unitPriceObj.Value : 0;
                    cartItemDto.SubtotalPrice = cartItemDto.UnitPrice * cartItemDto.Quantity;
                    return cartItemDto;
                });

            return response;

        }
    }
}
