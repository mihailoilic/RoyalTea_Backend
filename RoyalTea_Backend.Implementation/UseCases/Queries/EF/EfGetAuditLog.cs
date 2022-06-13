using AutoMapper;
using RoyalTea_Backend.Application.UseCases.DTO;
using RoyalTea_Backend.Application.UseCases.DTO.Searches;
using RoyalTea_Backend.Application.UseCases.Queries;
using RoyalTea_Backend.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.UseCases.Queries.EF
{
    public class EfGetAuditLog : EfUseCase, IGetAuditLog
    {
        public EfGetAuditLog(AppDbContext dbContext)
            :base(dbContext)
        {

        }
        public int Id => 33;

        public string Name => "Get Audit Log";

        public string Description => "Search Audit Log and see user actions";

        public PagedResponse<AuditLogDto> Execute(PagedAuditLogSearch request)
        {
            var keywords = request.Keywords;

            var query = this.DbContext.AuditLogs.AsQueryable();

            if (!String.IsNullOrWhiteSpace(keywords))
                query = query.Where(x => x.UseCaseName.ToLower().Contains(keywords.ToLower()) || x.Username.ToLower().Contains(keywords.ToLower()));

            if(!String.IsNullOrWhiteSpace(request.Username))
                query = query.Where(x => x.Username.ToLower().Contains(request.Username.ToLower()));

            if (!String.IsNullOrWhiteSpace(request.UseCaseName))
                query = query.Where(x => x.UseCaseName.ToLower().Contains(request.UseCaseName.ToLower()));

            if (request.DateFrom != null)
                query = query.Where(x => x.ExecutedAt > request.DateFrom);

            if (request.DateTo != null)
                query = query.Where(x => x.ExecutedAt < request.DateTo);

            var count = query.Count();

            var response = new PagedResponse<AuditLogDto>(request, count);
            response.Items = query.Select(x => Mapper.Map<AuditLogDto>(x)).Skip((request.PageNo - 1) * request.PerPage).Take(request.PerPage).ToList();

            return response;
        }
    }
}
