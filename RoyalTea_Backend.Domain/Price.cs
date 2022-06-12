using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Domain
{
    public class Price : Entity
    {
        public decimal Value { get; set; }
        public int CurrencyId { get; set; }
        public int ProductId { get; set; }

        public virtual Currency Currency { get; set; }
        public virtual Product Product { get; set; }
    }
}
