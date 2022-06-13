using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Application.UseCases.DTO.Specifications
{
    public class BaseSpecificationDto
    {
        public string Name { get; set; }

        public ICollection<BaseSpecificationValueDto> Values { get; set; }


    }
    public class SpecificationDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<SpecificationValueDto> Values { get; set; }
    }

    public class CreateSpecificationDto : BaseSpecificationDto 
    {
    }

    public class UpdateSpecificationDto : BaseSpecificationDto
    {
        public int Id { get; set; }
    }
    public class BaseSpecificationValueDto
    {
        public string Value { get; set; }

    }
    public class SpecificationValueDto : BaseSpecificationValueDto
    {
        public int Id { get; set; }
    }
}
