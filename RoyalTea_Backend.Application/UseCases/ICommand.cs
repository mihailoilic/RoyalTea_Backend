using System;
using System.Collections.Generic;
using System.Text;

namespace RoyalTea_Backend.Application.UseCases
{
    public interface ICommand<TRequest> : IUseCase
    {
        public void Execute(TRequest request);

    }

    public interface ICommand : IUseCase
    {
        public void Execute();

    }

}
