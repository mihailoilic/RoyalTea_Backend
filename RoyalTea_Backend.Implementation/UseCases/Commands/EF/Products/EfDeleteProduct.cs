using RoyalTea_Backend.Application.Exceptions;
using RoyalTea_Backend.Application.UseCases.Commands.Products;
using RoyalTea_Backend.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.UseCases.Commands.EF.Products
{
    public class EfDeleteProduct : EfUseCase, IDeleteProduct
    {
        public EfDeleteProduct(AppDbContext dbContext)
            : base(dbContext)
        {

        }
        public int Id => 29;

        public string Name => "Delete Product";
        public string Description => "Soft Delete Product";

        public void Execute(int request)
        {
            var product = this.DbContext.Products.FirstOrDefault(x => x.IsActive && x.Id == request);

            if (product == null)
                throw new EntityNotFoundException();

            product.IsActive = false;
            this.DbContext.SaveChanges();
        }
    }
}
