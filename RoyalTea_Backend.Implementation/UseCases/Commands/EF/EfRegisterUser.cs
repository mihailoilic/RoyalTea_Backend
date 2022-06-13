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
        UserValidator userValidator;
        public EfRegisterUser(AppDbContext dbContext, RegisterUserValidator validator, UserValidator userValidator)
            : base(dbContext)
        {
            this.validator = validator;
            this.userValidator = userValidator;
        }
        public int Id => 2;

        public string Name => "User Registration";

        public string Description => "Create new user with user data";

        public void Execute(RegisterDto request)
        {
            userValidator.ValidateAndThrow(request);
            validator.ValidateAndThrow(request);


            request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var userEntry = this.DbContext.Users.Add(Mapper.Map<User>(request));

            var useCaseIds = new List<int> {15, 18, 22, 25, 26};
            for (int id = 1; id < 14; id++)
                useCaseIds.Add(id);
            
            foreach(var id in useCaseIds)
                userEntry.Entity.UseCases.Add(new UseCase { UseCaseId = id });

            this.DbContext.SaveChanges();
        }
    }
}
