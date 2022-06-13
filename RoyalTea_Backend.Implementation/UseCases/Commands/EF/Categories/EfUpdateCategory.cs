using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RoyalTea_Backend.Application.Exceptions;
using RoyalTea_Backend.Application.UseCases.Commands.Categories;
using RoyalTea_Backend.Application.UseCases.DTO.Categories;
using RoyalTea_Backend.DataAccess;
using RoyalTea_Backend.Domain;
using RoyalTea_Backend.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.UseCases.Commands.EF.Categories
{
    public class EfUpdateCategory : EfUseCase, IUpdateCategory
    {
        public CategoryValidator validator { get; set; }

        public EfUpdateCategory(AppDbContext dbContext, CategoryValidator validator)
            : base(dbContext)
        {
            this.validator = validator;
        }
        public int Id => 20;

        public string Name => "Update Category";
        public string Description => "Update Category and its attached specifications";

        public void Execute(UpdateCategoryDto request)
        {
            this.validator.ValidateAndThrow(request);

            var category = this.DbContext.Categories.Include(x => x.CategorySpecifications).FirstOrDefault(x => x.Id == request.Id);
            if (category == null)
                throw new EntityNotFoundException();

            category.Name = request.Name;
            this.DbContext.CategorySpecifications.RemoveRange(category.CategorySpecifications);
            category.CategorySpecifications = request.SpecificationIds.Select(x => new CategorySpecification { SpecificationId = x }).ToList();

            this.DbContext.SaveChanges();


        }
    }
}
