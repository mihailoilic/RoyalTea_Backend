using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Domain
{
    public class Order : Entity
    {
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public bool IsCancelled { get; set; }

        public int OrderStatusId { get; set; }

        public virtual User User { get; set; }
        public virtual Address Address { get; set; }

        public virtual OrderStatus OrderStatus { get; set; }
    }
}
