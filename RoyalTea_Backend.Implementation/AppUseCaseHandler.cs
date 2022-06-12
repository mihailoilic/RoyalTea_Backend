using Newtonsoft.Json;
using RoyalTea_Backend.Application;
using RoyalTea_Backend.Application.Exceptions;
using RoyalTea_Backend.Application.Logging;
using RoyalTea_Backend.Application.UseCases;
using RoyalTea_Backend.Domain;
using System;
using System.Diagnostics;

namespace RoyalTea_Backend.Implementation
{
    public class AppUseCaseHandler : IUseCaseHandler
    {
        private IAppUser appUser;
        private IUseCaseLogger logger;
        private IExceptionLogger exceptionLogger;

        public AppUseCaseHandler(
            IAppUser appUser,
            IUseCaseLogger logger,
            IExceptionLogger exceptionLogger)
        {
            this.appUser = appUser;
            this.logger = logger;
            this.exceptionLogger = exceptionLogger;
        }


        public void Handle(ICommand command)
        {
            this.ApplyCrossCuttingConcerns(command, null, () => command.Execute());
        }

        public void Handle<TRequest>(ICommand<TRequest> command, TRequest data)
        {
            this.ApplyCrossCuttingConcerns(command, null, () => command.Execute(data));
        }

        public TResponse Handle<TResponse>(IQuery<TResponse> query)
        {
            TResponse response = default;

            this.ApplyCrossCuttingConcerns(query, null, () => response = query.Execute());

            return response;
        }

        public TResponse Handle<TRequest, TResponse>(IQuery<TRequest, TResponse> query, TRequest data)
        {
            TResponse response = default;

            this.ApplyCrossCuttingConcerns(query, null, () => response = query.Execute(data));

            return response;
        }



        protected void ApplyCrossCuttingConcerns(IUseCase usecase, object data, Action action)
        {
            try
            {
                this.UserActivityCheck(usecase, data);

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                action();

                stopwatch.Stop();

                Console.WriteLine("Duration " + stopwatch.ElapsedMilliseconds + "ms");

            }
            catch(Exception ex)
            {
                this.exceptionLogger.Log(ex);
                throw;
            }
        }

        protected void UserActivityCheck(IUseCase useCase, object data)
        {
            bool isAuthorized = this.appUser.UseCaseIds.Contains(useCase.Id);

            this.logger.Log(new UseCaseLog
            {
                UseCaseName = useCase.Name,
                Username = this.appUser.Username,
                UserId = this.appUser.Id,
                ExecutedAt = DateTime.UtcNow,
                IsAuthorized = isAuthorized,
                Data = JsonConvert.SerializeObject(data)
            });

            if(!isAuthorized)
            {
                throw new ForbiddenUseCaseException(useCase.Name, this.appUser.Username);
            }

        }
    }
}
