using System;
using System.Collections.Generic;
using System.Text;

namespace RoyalTea_Backend.Application.UseCases
{
    public interface IUseCase
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
    }
}
