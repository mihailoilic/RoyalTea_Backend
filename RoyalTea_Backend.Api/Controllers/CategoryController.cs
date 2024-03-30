using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoyalTea_Backend.Application;
using RoyalTea_Backend.Application.UseCases.Commands.Categories;
using RoyalTea_Backend.Application.UseCases.DTO.Categories;
using RoyalTea_Backend.Application.UseCases.DTO.Searches;
using RoyalTea_Backend.Application.UseCases.Queries.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RoyalTea_Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public IUseCaseHandler handler { get; set; }

        public CategoryController(IUseCaseHandler handler)
        {
            this.handler = handler;
        }


        // GET: api/<CategoryController>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get([FromQuery] PagedSearch request, [FromServices] IGetCategories query)
        {
            return Ok(this.handler.Handle(query, request));
        }


        // POST api/<CategoryController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateCategoryDto request, [FromServices] ICreateCategory command)
        {
            this.handler.Handle(command, request);

            return StatusCode(201);
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateCategoryDto request, [FromServices] IUpdateCategory command)
        {
            request.Id = id;
            this.handler.Handle(command, request);

            return StatusCode(204);
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteCategory command)
        {
            this.handler.Handle(command, id);

            return StatusCode(204);
        }
    }
}
