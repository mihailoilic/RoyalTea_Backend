using RoyalTea_Backend.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Api.Core
{
    public class JwtAppUser : IAppUser
    {
        public string Username { get; set; }
        public int Id { get; set; }
        public ICollection<int> UseCaseIds { get; set; }
    }


    public class AnonymousAppUser : IAppUser
    {
        public string Username => "(guest)";
        public int Id => 0;
        public ICollection<int> UseCaseIds => new List<int> { 1, 2, 3 };
    }
}
