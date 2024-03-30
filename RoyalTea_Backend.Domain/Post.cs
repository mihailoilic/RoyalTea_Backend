using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Domain
{
    public class Post : Entity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public virtual Image Image { get; set; }
        public int Views { get; set; }

    }
}
