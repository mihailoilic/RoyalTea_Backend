using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RoyalTea_Backend.Application.Exceptions;
using RoyalTea_Backend.Application.UseCases.Commands.Users;
using RoyalTea_Backend.Application.UseCases.DTO;
using RoyalTea_Backend.DataAccess;
using RoyalTea_Backend.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.UseCases.Commands.EF.Users
{
    public class EfUpdateUser : EfUseCase, IUpdateUser
    {
        UserValidator validator;

        public EfUpdateUser(AppDbContext dbContext, UserValidator validator)
            : base(dbContext)
        {
            this.validator = validator;
        }
        public int Id => 31;

        public string Name => "Update user";

        public string Description => "Update User Info";

        public void Execute(UserDto request)
        {
            this.validator.ValidateAndThrow(request);

            var user = this.DbContext.Users.Include(x => x.UseCases).Where(x => x.IsActive).FirstOrDefault(x => x.Id == request.Id);
            if (user == null)
                throw new EntityNotFoundException();

            this.DbContext.UseCases.RemoveRange(user.UseCases);
            Mapper.Map(request, user);

            this.DbContext.SaveChanges();
        }
    }
}
