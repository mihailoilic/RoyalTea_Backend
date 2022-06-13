using Microsoft.AspNetCore.Mvc;
using RoyalTea_Backend.Application;
using RoyalTea_Backend.Application.UseCases.Commands.Orders;
using RoyalTea_Backend.Application.UseCases.DTO.Order;
using RoyalTea_Backend.Application.UseCases.DTO.Searches;
using RoyalTea_Backend.Application.UseCases.Queries.Orders;
using RoyalTea_Backend.Implementation.UseCases.Commands.EF.Orders;
using RoyalTea_Backend.Implementation.UseCases.Queries.EF.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RoyalTea_Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public IUseCaseHandler handler { get; set; }

        public OrderController(IUseCaseHandler handler)
        {
            this.handler = handler;
        }

        // GET: api/<OrderController>
        [HttpGet]
        public IActionResult Get([FromQuery] PagedOrderSearch request, [FromServices] IGetOrders query)
        {
            
            return Ok(this.handler.Handle(query, request));
        }


        // POST api/<OrderController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateOrderDto request, [FromServices] ICreateOrder command)
        {
            this.handler.Handle(command, request);

            return StatusCode(201);
        }


        // === Set status to Cancelled ====
        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] ICancelOrder command)
        {
            this.handler.Handle(command, id);

            return StatusCode(204);
        }
    }
}
