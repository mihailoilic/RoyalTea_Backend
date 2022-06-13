using Microsoft.AspNetCore.Mvc;
using RoyalTea_Backend.Application;
using RoyalTea_Backend.Application.UseCases.Commands.Cart;
using RoyalTea_Backend.Application.UseCases.DTO;
using RoyalTea_Backend.Application.UseCases.DTO.Searches;
using RoyalTea_Backend.Application.UseCases.Queries.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RoyalTea_Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        public IUseCaseHandler  handler { get; set; }

        public CartController(IUseCaseHandler handler)
        {
            this.handler = handler;
        }



        // GET: api/<CartController>
        [HttpGet]
        public IActionResult Get([FromQuery] PagedCartItemSearch request, [FromServices] IGetCartItems query)
        {
            return Ok(this.handler.Handle(query, request));
        }


        // POST api/<CartController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateCartItemDto request, [FromServices] ICreateCartItem command)
        {
            this.handler.Handle(command, request);

            return StatusCode(201);
        }

        // PUT api/<CartController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateCartItemDto request, [FromServices] IUpdateCartItem command)
        {
            request.ProductId = id;
            this.handler.Handle(command, request);

            return StatusCode(204);

        }

        // DELETE api/<CartController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteCartItem command)
        {
            this.handler.Handle(command, id);

            return StatusCode(204);
        }
    }
}
