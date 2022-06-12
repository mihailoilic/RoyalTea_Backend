using System;
using System.Collections.Generic;
using System.Text;

namespace RoyalTea_Backend.Application.UseCases
{
    public interface IQuery<TRequest, TResponse> : IUseCase
    {
        public TResponse Execute(TRequest request);
    }
    public interface IQuery<TResponse> : IUseCase
    {
        public TResponse Execute();
    }
}
