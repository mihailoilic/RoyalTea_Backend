using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Application.UseCases.DTO.Searches
{
    public class Search
    {
        public string Keywords { get; set; }
    }

    public class PagedSearch : Search
    {
        private int pageNo = 1;
        private int perPage = 10;
        public int PageNo
        {
            get => this.pageNo;
            set 
            {
                if(value < 1)
                {
                    value = 1;
                }
                this.pageNo = value;
            }
        }
        public int PerPage
        {
            get => this.perPage;
            set
            {
                if (value < 10)
                {
                    value = 10;
                }
                this.perPage = value;
            }
        }

    }
}
