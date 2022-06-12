using RoyalTea_Backend.Application.UseCases.DTO.Searches;
using System;
using System.Collections.Generic;

namespace RoyalTea_Backend.Application.UseCases.DTO
{
    public class PagedResponse<T>
    {
        public int PageNo { get; set; }
        public int PerPage { get; set; }
        public int NoOfPages { get; set; }
        public int TotalItems { get; set; }

        public IEnumerable<T> Items { get; set; }

        public PagedResponse(PagedSearch request, int count)
        {
            this.PageNo = request.PageNo;
            this.PerPage = request.PerPage;
            this.NoOfPages = (int)Math.Ceiling((decimal)count / request.PerPage);
            this.TotalItems = count;
        }
    }
}
