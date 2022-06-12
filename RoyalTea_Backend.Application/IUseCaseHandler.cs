using RoyalTea_Backend.Application.UseCases;
using System;

namespace RoyalTea_Backend.Application
{
    public interface IUseCaseHandler
    {

        public void Handle(ICommand command);
        public void Handle<TRequest>(ICommand<TRequest> command, TRequest data);
        public TResponse Handle<TResponse>(IQuery<TResponse> query);
        public TResponse Handle<TRequest, TResponse>(IQuery<TRequest, TResponse> query, TRequest data);


    }
}
