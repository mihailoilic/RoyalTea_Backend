using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Domain
{
    public class Address : Entity
    {
        public string DeliveryLocation { get; set; }
        public int CountryId { get; set; }
        public int UserId { get; set; }

        public virtual Country Country { get; set; }
        public virtual User User { get; set; }
    }
}
