using AutoMapper;
using FluentValidation;
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
    public class EfCreateSpecification : EfUseCase, ICreateSpecification
    {
        public SpecificationValidator validator { get; set; }
        public EfCreateSpecification(AppDbContext dbContext, SpecificationValidator validator)
            : base(dbContext)
        {
            this.validator = validator;
        }
        public int Id => 14;

        public string Name => "Create Specification";

        public string Description => "Add new specification with values";

        public void Execute(CreateSpecificationDto request)
        {
            this.validator.ValidateAndThrow(request);

            var specification = Mapper.Map<Specification>(request);
            specification.SpecificationValues = request.Values.Select(x => Mapper.Map<SpecificationValue>(x)).ToList();

            this.DbContext.Specifications.Add(specification);
            this.DbContext.SaveChanges();



        }
    }
}
