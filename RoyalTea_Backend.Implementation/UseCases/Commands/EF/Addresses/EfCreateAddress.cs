using AutoMapper;
using FluentValidation;
using RoyalTea_Backend.Application.UseCases.Commands.Addresses;
using RoyalTea_Backend.Application.UseCases.DTO;
using RoyalTea_Backend.DataAccess;
using RoyalTea_Backend.Domain;
using RoyalTea_Backend.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.UseCases.Commands.EF.Addresses
{
    public class EfCreateAddress : EfUseCase, ICreateAddress
    {
        AddressValidator validator;
        public EfCreateAddress(AppDbContext dbContext, AddressValidator validator)
            : base(dbContext)
        {
            this.validator = validator;
        }
        public int Id => 3;

        public string Name => "Create Address";

        public string Description => "Users can add their delivery addresses.";

        public void Execute(AddAddressDto request)
        {
            this.validator.ValidateAndThrow(request);

            var user = this.DbContext.Users.FirstOrDefault(x => x.Id == this.DbContext.AppUser.Id);
            if (user == null)
                throw new UnauthorizedAccessException();
            

            user.Addresses.Add(Mapper.Map<Address>(request));
            DbContext.SaveChanges();
        }
    }
}
