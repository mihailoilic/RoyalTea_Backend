using FluentValidation;
using RoyalTea_Backend.Application.Exceptions;
using RoyalTea_Backend.Application.UseCases.Commands.Addresses;
using RoyalTea_Backend.Application.UseCases.DTO;
using RoyalTea_Backend.DataAccess;
using RoyalTea_Backend.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.UseCases.Commands.EF.Addresses
{
    public class EfUpdateAddress : EfUseCase, IUpdateAddress
    {
        public AddressValidator validator { get; set; }

        public EfUpdateAddress(AddressValidator validator, AppDbContext dbContext)
            :base(dbContext)
        {
            this.validator = validator;
        }

        public int Id => 4;

        public string Name => "Update Address";

        public string Description => "User can edit one of their own addresses";

        public void Execute(UpdateAddressDto request)
        {
            validator.ValidateAndThrow(request);

            var address = this.DbContext.Addresses.FirstOrDefault(x => x.Id == request.Id && x.UserId == this.DbContext.AppUser.Id);
            if (address == null)
                throw new EntityNotFoundException();

            address.DeliveryLocation = request.DeliveryLocation;
            address.CountryId = request.CountryId;

            this.DbContext.SaveChanges();
        }
    }
}
