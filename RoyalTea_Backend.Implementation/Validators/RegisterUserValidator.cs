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
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid Email format.")
                .Must(x => !dbContext.Users.Any(u => u.Email == x)).WithMessage("Email address {PropertyValue} has already been used.");

            RuleFor(x => x.Username)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Username is requred.")
                .MinimumLength(3).WithMessage("Minimum characters: 3.")
                .MaximumLength(20).WithMessage("Maximum characters: 20.")
                .Matches("^(?=[a-zA-Z0-9._]{3,20}$)(?!.*[_.]{2})[^_.].*[^_.]$")
                .WithMessage("Invalid Username.")
                .Must(x => !dbContext.Users.Any(u => u.Username == x)).WithMessage("Username {PropertyValue} is already taken.");

            var imePrezimeRegex = @"^\p{Lu}\p{Ll}{1,20}(\s\p{L}{2,20}){1,}$";
            RuleFor(x => x.FullName).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Full Name is required.")
                .Matches(imePrezimeRegex).WithMessage("Full Name is invalid.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
                .WithMessage("Password has to be at least 8 characters long and include at least one lowercase, one uppercase letter, one number and one special characer.");
        }
    }
}
