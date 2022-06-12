using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Domain
{
    public interface IAppUser
    {
        public string Username { get; }
        public int Id { get; }
        public ICollection<int> UseCaseIds { get; }
    }
}
