using Microsoft.AspNetCore.Http;
using RoyalTea_Backend.Application.UseCases.DTO;
using RoyalTea_Backend.Application.UseCases.DTO.Categories;
using RoyalTea_Backend.Application.UseCases.DTO.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Application.UseCases.DTO.Products
{
    public class BaseProductDto : BaseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
    public class ProductDto : BaseProductDto
    {
        public ICollection<PriceDto> Prices { get; set; }
        public ICollection<SpecificationDto> ProductSpecifications { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Image { get; set; }
        public BaseCategoryDto Category { get; set; }

    }

    public class CreateProductDto : BaseProductDto
    {
        public ICollection<int> SpecificationValueIds { get; set; }
        public IFormFile ImageFile { get; set; }
        public int CategoryId { get; set; }
        public ICollection<PriceDto> Prices { get; set; }

    }

    public class UpdateProductDto : CreateProductDto { }

    public class SerializedCreateProductDto : BaseProductDto
    {
        public ICollection<int> SpecificationValueIds { get; set; }
        public IFormFile ImageFile { get; set; }
        public int CategoryId { get; set; }
        public ICollection<string> Prices { get; set; }
    }

    public class SerializedUpdateProductDto : SerializedCreateProductDto { }



    public class PriceDto : BaseDto
    {
        public string CurrencyIso { get; set; }
        public decimal Value { get; set; }

    }


}
