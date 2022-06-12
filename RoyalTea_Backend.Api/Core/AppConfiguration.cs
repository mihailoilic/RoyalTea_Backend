using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Api.Core
{
    public class AppConfiguration
    {
        public string ConnectionString { get; set; }
        public JwtConfiguration JwtConfig { get; set; }
    }

    public class JwtConfiguration
    {
        public int Duration { get; set; }
        public string Issuer { get; set; }
        public string PrivateKey { get; set; }
    }
}
