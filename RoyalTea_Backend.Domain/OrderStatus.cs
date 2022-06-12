using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Domain
{
    public class OrderStatus : Entity
    {
        public string Name { get; set; }
        public bool IsCancellable { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
