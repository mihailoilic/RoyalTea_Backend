using FluentValidation;
using RoyalTea_Backend.Application.UseCases.DTO;
using RoyalTea_Backend.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterDto>
    {
        public RegisterUserValidator(AppDbContext dbContext)
        {
            RuleFor(x => x).Cascade(CascadeMode.Stop)
                .Must(x => !dbContext.Users.Any(u => u.Email == x.Email)).WithMessage("Email address has already been used.");

            RuleFor(x => x.Username)
                .Must(x => !dbContext.Users.Any(u => u.Username == x)).WithMessage("Username {PropertyValue} is already taken.");


            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
                .WithMessage("Password has to be at least 8 characters long and include at least one lowercase, one uppercase letter, one number and one special characer.");
        }
    }
}
