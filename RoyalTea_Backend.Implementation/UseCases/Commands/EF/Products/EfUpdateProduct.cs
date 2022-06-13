using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RoyalTea_Backend.Application.Exceptions;
using RoyalTea_Backend.Application.UseCases.Commands.Products;
using RoyalTea_Backend.Application.UseCases.DTO.Products;
using RoyalTea_Backend.DataAccess;
using RoyalTea_Backend.Domain;
using RoyalTea_Backend.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.UseCases.Commands.EF.Products
{
    public class EfUpdateProduct : EfUseCase, IUpdateProduct
    {
        public ProductValidator validator { get; set; }

        public EfUpdateProduct(AppDbContext dbContext, ProductValidator validator)
            : base(dbContext)
        {
            this.validator = validator;
        }
        public int Id => 28;

        public string Name => "Update product";

        public string Description => "Update product along with its category, prices and specification values";

        public void Execute(UpdateProductDto request)
        {
            this.validator.ValidateAndThrow(request);

            var product = this.DbContext.Products.Include(x => x.Image)
                .Include(x => x.Category).ThenInclude(x => x.CategorySpecifications).ThenInclude(x => x.Specification)
                .Include(x => x.ProductSpecificationValues)
                .Include(x => x.Prices).ThenInclude(x => x.Currency).FirstOrDefault(x => x.IsActive && x.Id == request.Id);

            if (product == null)
                throw new EntityNotFoundException();

            this.DbContext.Prices.RemoveRange(product.Prices);
            this.DbContext.ProductSpecificationValues.RemoveRange(product.ProductSpecificationValues);
            this.DbContext.Images.Remove(product.Image);

            Mapper.Map(request, product);
            product.Image = this.DbContext.Images.Add(new Image { Path = request.Image }).Entity;
            product.Prices = request.Prices.Select(x => new Price
            {
                Currency = this.DbContext.Currencies.FirstOrDefault(c => c.IsActive && c.IsoCode == x.CurrencyIso),
                Value = x.Value
            }).ToList();

            this.DbContext.SaveChanges();


        }
    }
}
