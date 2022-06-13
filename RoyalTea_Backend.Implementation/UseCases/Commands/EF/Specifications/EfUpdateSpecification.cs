using AutoMapper;
using FluentValidation;
using RoyalTea_Backend.Application.Exceptions;
using RoyalTea_Backend.Application.UseCases.Commands.Specifications;
using RoyalTea_Backend.Application.UseCases.DTO.Specifications;
using RoyalTea_Backend.DataAccess;
using RoyalTea_Backend.Domain;
using RoyalTea_Backend.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.UseCases.Commands.EF.Specifications
{
    public class EfUpdateSpecification : EfUseCase, IUpdateSpecification
    {
        public SpecificationValidator validator { get; set; }

        public EfUpdateSpecification(AppDbContext dbContext, SpecificationValidator validator)
            : base(dbContext)
        {
            this.validator = validator;
        }
        public int Id => 16;

        public string Name => "Update Specification";

        public string Description => "Update specification and its values";

        public void Execute(UpdateSpecificationDto request)
        {
            this.validator.ValidateAndThrow(request);

            var specification = this.DbContext.Specifications.FirstOrDefault(x => x.Id == request.Id);
            if (specification == null)
                throw new EntityNotFoundException();

            specification.Name = request.Name;
            this.DbContext.SpecificationValues.RemoveRange(specification.SpecificationValues);
            specification.SpecificationValues = request.Values.Select(x => Mapper.Map<SpecificationValue>(x)).ToList();

            this.DbContext.SaveChanges();
        }
    }
}
