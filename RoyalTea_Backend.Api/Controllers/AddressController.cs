using Microsoft.AspNetCore.Mvc;
using RoyalTea_Backend.Application;
using RoyalTea_Backend.Application.UseCases.Commands.Addresses;
using RoyalTea_Backend.Application.UseCases.DTO;
using RoyalTea_Backend.Application.UseCases.DTO.Searches;
using RoyalTea_Backend.Application.UseCases.Queries.Addresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RoyalTea_Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        public IUseCaseHandler handler { get; set; }

        public AddressController(IUseCaseHandler handler)
        {
            this.handler = handler;
        }


        // GET: api/<AddressController>
        [HttpGet]
        public IActionResult Get([FromQuery] PagedSearch request, [FromServices] IGetAddresses query)
        {
            return Ok(this.handler.Handle(query, request));
        }


        // POST api/<AddressController>
        [HttpPost]
        public IActionResult Post([FromBody] AddAddressDto request, [FromServices] ICreateAddress command)
        {
            this.handler.Handle(command, request);

            return StatusCode(201);
        }

        // PUT api/<AddressController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateAddressDto request, [FromServices] IUpdateAddress command)
        {
            request.Id = id;
            this.handler.Handle(command, request);

            return StatusCode(204);
        }

        // DELETE api/<AddressController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteAddress command)
        {
            this.handler.Handle(command, id);

            return StatusCode(204);
        }
    }
}
