using Microsoft.EntityFrameworkCore;
using RoyalTea_Backend.Application.Exceptions;
using RoyalTea_Backend.Application.UseCases.Commands.Categories;
using RoyalTea_Backend.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.UseCases.Commands.EF.Categories
{
    public class EfDeleteCategory : EfUseCase, IDeleteCategory
    {
        public EfDeleteCategory(AppDbContext dbContext)
            : base(dbContext)
        {

        }
        public int Id => 21;

        public string Name => "Delete Category";

        public string Description => "Soft Delete Category";

        public void Execute(int request)
        {
            var category = this.DbContext.Categories.Include(x => x.CategorySpecifications).FirstOrDefault(x => x.Id == request);
            if (category == null)
                throw new EntityNotFoundException();

            category.IsActive = false;
            this.DbContext.SaveChanges();
        }
    }
}
