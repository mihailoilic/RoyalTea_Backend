using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Api.Core.DTO
{
    public class TokenRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
