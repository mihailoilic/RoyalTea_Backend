using AutoMapper;
using FluentValidation;
using RoyalTea_Backend.Application.UseCases.Commands;
using RoyalTea_Backend.Application.UseCases.DTO;
using RoyalTea_Backend.DataAccess;
using RoyalTea_Backend.Domain;
using RoyalTea_Backend.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.UseCases.Commands.EF
{
    public class EfRegisterUser : EfUseCase, IRegisterUser
    {
        RegisterUserValidator validator;
        public EfRegisterUser(AppDbContext dbContext, RegisterUserValidator validator)
            : base(dbContext)
        {
            this.validator = validator;
        }
        public int Id => 2;

        public string Name => "User Registration";

        public string Description => "Create new user with user data";

        public void Execute(RegisterDto request)
        {
            validator.ValidateAndThrow(request);

            request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var userEntry = this.DbContext.Users.Add(Mapper.Map<User>(request));

            for(int id = 1; id < 4; id++)
                userEntry.Entity.UseCases.Add(new UseCase { UseCaseId = id });

            this.DbContext.SaveChanges();
        }
    }
}
