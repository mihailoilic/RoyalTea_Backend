using Microsoft.AspNetCore.Mvc;
using RoyalTea_Backend.Application;
using RoyalTea_Backend.Application.UseCases.Commands.Specifications;
using RoyalTea_Backend.Application.UseCases.DTO.Searches;
using RoyalTea_Backend.Application.UseCases.DTO.Specifications;
using RoyalTea_Backend.Application.UseCases.Queries.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RoyalTea_Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecificationController : ControllerBase
    {

        public IUseCaseHandler handler { get; set; }

        public SpecificationController(IUseCaseHandler handler)
        {
            this.handler = handler;
        }

        // GET: api/<SpecificationController>
        [HttpGet]
        public IActionResult Get([FromQuery] PagedSearch request, [FromServices] IGetSpecifications query)
        {
            return Ok(this.handler.Handle(query, request));
        }



        // POST api/<SpecificationController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateSpecificationDto request, [FromServices] ICreateSpecification command)
        {
            this.handler.Handle(command, request);

            return StatusCode(201);
        }

        // PUT api/<SpecificationController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateSpecificationDto request, [FromServices] IUpdateSpecification command)
        {
            request.Id = id;
            this.handler.Handle(command, request);

            return StatusCode(204);
        }

        // DELETE api/<SpecificationController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteSpecification command)
        {
            this.handler.Handle(command, id);

            return StatusCode(204);
        }
    }
}
