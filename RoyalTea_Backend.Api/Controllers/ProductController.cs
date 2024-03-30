using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RoyalTea_Backend.Application;
using RoyalTea_Backend.Application.UseCases.Commands.Products;
using RoyalTea_Backend.Application.UseCases.DTO.Products;
using RoyalTea_Backend.Application.UseCases.DTO.Searches;
using RoyalTea_Backend.Application.UseCases.Queries.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RoyalTea_Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public IUseCaseHandler handler { get; set; }

        public ProductController(IUseCaseHandler handler)
        {
            this.handler = handler;
        }


        // GET: api/<ProductController>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get([FromQuery] PagedSearch request, [FromServices] IGetProducts query)
        {

            return Ok(this.handler.Handle(query, request));
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetProduct query)
        {

            return Ok(this.handler.Handle(query, id));
        }

        // POST api/<ProductController>
        [HttpPost]
        public IActionResult Post([FromForm] SerializedCreateProductDto request, [FromServices] ICreateProduct command)
        {
            var decoded = Mapper.Map<CreateProductDto>(request);

            this.handler.Handle(command, decoded);

            return StatusCode(201);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromForm] SerializedUpdateProductDto request, [FromServices] IUpdateProduct command)
        {
            var decoded = Mapper.Map<UpdateProductDto>(request);

            decoded.Id = id;
            this.handler.Handle(command, decoded);

            return StatusCode(204);
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteProduct command)
        {
            this.handler.Handle(command, id);

            return StatusCode(204);
        }
    }
}
