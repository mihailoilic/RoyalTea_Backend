using AutoMapper;
using RoyalTea_Backend.Application.UseCases.DTO;
using RoyalTea_Backend.Application.UseCases.DTO.Searches;
using RoyalTea_Backend.Application.UseCases.Queries;
using RoyalTea_Backend.DataAccess;
using RoyalTea_Backend.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RoyalTea_Backend.Implementation.UseCases.Queries.EF
{
    public class EfGetUsersQuery : EfUseCase, IGetUsersQuery
    {
        public EfGetUsersQuery(AppDbContext dbContext)
            : base(dbContext)
        {
        }
        public int Id => 50;

        public string Name => "Search users";

        public string Description => "Get all users and filter by keywords";

        public PagedResponse<UserDto> Execute(PagedSearch request)
        {
            var keywords = request.Keywords;

            var query = this.DbContext.Users.Include(x => x.UseCases).AsQueryable();

            if (keywords != null)
                query = query.Where(x => x.FullName.Contains(keywords) || x.Username.Contains(keywords) || x.Email.Contains(keywords));

            var count = query.Count();

            var response = new PagedResponse<UserDto>(request, count);
            response.Items = query.Select(x => Mapper.Map<UserDto>(x)).Skip((request.PageNo - 1) * request.PerPage).Take(request.PerPage).ToList();

            return response;

        }
    }
}
