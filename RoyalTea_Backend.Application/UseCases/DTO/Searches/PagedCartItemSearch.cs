using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Application.UseCases.DTO.Searches
{
    public class PagedCartItemSearch : PagedSearch
    {
        public int CurrencyId { get; set; } = 1;
    }
}
