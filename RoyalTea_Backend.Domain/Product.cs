using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Domain
{
    public class Product : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageId { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Image Image { get; set; }
        public virtual ICollection<Price> Prices { get; set; }
        public virtual ICollection<ProductSpecificationValue> ProductSpecificationValues { get; set; }
    }
}
