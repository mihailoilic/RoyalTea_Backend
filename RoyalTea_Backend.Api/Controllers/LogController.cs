using Microsoft.AspNetCore.Mvc;
using RoyalTea_Backend.Application;
using RoyalTea_Backend.Application.UseCases.DTO.Searches;
using RoyalTea_Backend.Application.UseCases.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RoyalTea_Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        public IUseCaseHandler handler { get; set; }

        public LogController(IUseCaseHandler handler)
        {
            this.handler = handler;
        }

        // GET: api/<LogController>
        [HttpGet]
        public IActionResult Get([FromQuery] PagedAuditLogSearch request, [FromServices] IGetAuditLog query)
        {
            return Ok(this.handler.Handle(query, request));
        }

    }
}
