using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Domain
{
    public class Slide : Entity
    {
        public int ImageId { get; set; }
        public string HtmlContent { get; set; }

        public virtual Image Image { get; set; }
    }
}
