using AutoMapper;
using FluentValidation;
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
    public class EfCreateProduct : EfUseCase, ICreateProduct
    {
        public ProductValidator validator { get; set; }
        public EfCreateProduct(AppDbContext dbContext, ProductValidator validator)
            : base(dbContext)
        {
            this.validator = validator;
        }
        public int Id => 27;

        public string Name => "Create product";
        public string Description => "Create new product with prices and specification values";


        public void Execute(CreateProductDto request)
        {
            this.validator.ValidateAndThrow(request);

            var product = Mapper.Map<Product>(request);
            product.Image = this.DbContext.Images.Add(new Image { Path = request.Image }).Entity;
            product.Prices = request.Prices.Select(x => new Price 
            { 
                Currency = this.DbContext.Currencies.FirstOrDefault(c => c.IsActive && c.IsoCode == x.CurrencyIso),
                Value = x.Value
            }).ToList();

            this.DbContext.Products.Add(product);
            this.DbContext.SaveChanges();
        }
    }
}
