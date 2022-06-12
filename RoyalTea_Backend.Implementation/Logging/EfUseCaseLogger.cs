using AutoMapper;
using RoyalTea_Backend.Application;
using RoyalTea_Backend.Application.Logging;
using RoyalTea_Backend.DataAccess;
using RoyalTea_Backend.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.Logging
{
    public class EfUseCaseLogger : IUseCaseLogger
    {
        public AppDbContext dbContext { get; set; }

        public EfUseCaseLogger(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<UseCaseLog> GetLogs(UseCaseLogSearch search)
        {
            throw new NotImplementedException();
        }

        public void Log(UseCaseLog log)
        {
            this.dbContext.AuditLogs.Add(Mapper.Map<AuditLog>(log));
            this.dbContext.SaveChanges();

            Console.WriteLine($"User: {log.Username} - UseCase: {log.UseCaseName}");

        }
    }
}
