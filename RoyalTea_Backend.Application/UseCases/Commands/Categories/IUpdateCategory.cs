using RoyalTea_Backend.Application.UseCases.DTO.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Application.UseCases.Commands.Categories
{
    public interface IUpdateCategory : ICommand<UpdateCategoryDto>
    {
    }
}
