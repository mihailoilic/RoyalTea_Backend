using RoyalTea_Backend.Application.Exceptions;
using RoyalTea_Backend.Application.UseCases.Commands.Addresses;
using RoyalTea_Backend.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.UseCases.Commands.EF.Addresses
{
    public class EfDeleteAddress : EfUseCase, IDeleteAddress
    {
        public EfDeleteAddress(AppDbContext dbContext)
            : base(dbContext)
        {

        }

        public int Id => 5;

        public string Name => "Delete Address";

        public string Description => "Users can delete one of their addresses";

        public void Execute(int request)
        {
            var address = this.DbContext.Addresses.FirstOrDefault(x => x.Id == request && x.UserId == this.DbContext.AppUser.Id);
            if (address == null)
                throw new EntityNotFoundException();

            address.IsActive = false;
            DbContext.SaveChanges();

        }
    }
}
