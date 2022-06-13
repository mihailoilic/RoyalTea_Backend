using RoyalTea_Backend.Application.Exceptions;
using RoyalTea_Backend.Application.UseCases.Commands.Specifications;
using RoyalTea_Backend.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.UseCases.Commands.EF.Specifications
{
    public class EfDeleteSpecification : EfUseCase, IDeleteSpecification
    {
        public EfDeleteSpecification(AppDbContext dbContext)
            : base(dbContext)
        {

        }
        public int Id => 17;

        public string Name => "Delete specification";

        public string Description => "Soft delete specification";

        public void Execute(int request)
        {
            var specification = this.DbContext.Specifications.FirstOrDefault(x => x.Id == request);
            if (specification == null)
                throw new EntityNotFoundException();

            specification.IsActive = false;
            this.DbContext.SaveChanges();
        }
    }
}
