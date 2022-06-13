using RoyalTea_Backend.Application.Exceptions;
using RoyalTea_Backend.Application.UseCases.Commands.Currencies;
using RoyalTea_Backend.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.UseCases.Commands.EF.Currencies
{
    public class EfDeleteCurrency : EfUseCase, IDeleteCurrency
    {
        public EfDeleteCurrency(AppDbContext dbContext)
            : base(dbContext)
        {

        }
        public int Id => 24;

        public string Name => "Delete Currency";

        public string Description => "Soft Delete Currency";

        public void Execute(int request)
        {
            var currency = this.DbContext.Currencies.FirstOrDefault(x => x.Id == request && x.IsActive);
            if (currency == null)
                throw new EntityNotFoundException();

            currency.IsActive = false;
            this.DbContext.SaveChanges();
        }
    }
}
