using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Domain
{
    public class Category : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<CategorySpecification> CategorySpecifications { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
