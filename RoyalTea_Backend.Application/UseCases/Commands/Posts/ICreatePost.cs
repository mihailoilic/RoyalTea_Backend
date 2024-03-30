using RoyalTea_Backend.Application.UseCases.DTO.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Application.UseCases.Commands.Posts
{
    public interface ICreatePost : ICommand<CreatePostDto>
    {
    }
}
