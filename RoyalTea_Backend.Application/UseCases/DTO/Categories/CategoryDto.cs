using RoyalTea_Backend.Application.UseCases.DTO.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Application.UseCases.DTO.Categories
{
    public class BaseCategoryDto : BaseDto
    {
        public string Name { get; set; }
    }

    public class CreateCategoryDto : BaseCategoryDto
    {
        public ICollection<int> SpecificationIds { get; set; }

    }

    public class UpdateCategoryDto : CreateCategoryDto
    {
        public int Id { get; set; }
    }

    public class CategoryDto : BaseCategoryDto
    {
        public int Id { get; set; }
        public ICollection<object>  Specifications { get; set; }

    }

}
