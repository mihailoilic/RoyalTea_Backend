using FluentValidation;
using RoyalTea_Backend.Application.UseCases.DTO.Specifications;
using RoyalTea_Backend.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.Validators
{
    public class SpecificationValidator : AbstractValidator<BaseSpecificationDto>
    {
        public SpecificationValidator(AppDbContext dbContext)
        {
            RuleFor(x => x).Cascade(CascadeMode.Stop)
                .Must(x => !(x is UpdateSpecificationDto) ? !dbContext.Specifications.Any(s => s.Name == x.Name) : true).WithMessage("Specification Name must be unique.");

            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Specification name is required.")
                .MinimumLength(3).WithMessage("Minimum length: 3")
                .MaximumLength(30).WithMessage("Maximum length: 30");



            RuleFor(x => x.Values).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Values are required")
                .Must(x => x.All(v => !String.IsNullOrWhiteSpace(v.Value))).WithMessage("Value must not be empty");
                
        }
    }
}
