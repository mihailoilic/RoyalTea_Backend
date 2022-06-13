﻿using RoyalTea_Backend.Application.UseCases.DTO;
using RoyalTea_Backend.Application.UseCases.DTO.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Application.UseCases.Queries
{
    public interface IGetAuditLog  : IQuery<PagedAuditLogSearch, PagedResponse<AuditLogDto>>
    {
    }
}
