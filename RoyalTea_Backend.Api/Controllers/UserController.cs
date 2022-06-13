using Microsoft.AspNetCore.Mvc;
using RoyalTea_Backend.Application;
using RoyalTea_Backend.Application.UseCases.Commands.Users;
using RoyalTea_Backend.Application.UseCases.DTO;
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
    public class UserController : ControllerBase
    {

        public IUseCaseHandler handler { get; set; }

        public UserController(IUseCaseHandler handler)
        {
            this.handler = handler;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get([FromQuery] PagedSearch request, [FromServices] IGetUsersQuery query)
        {
            var result = handler.Handle(query, request);
            return Ok(result);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserDto request, [FromServices] IUpdateUser command)
        {
            request.Id = id;
            this.handler.Handle(command, request);

            return StatusCode(204);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteUser command)
        {
            this.handler.Handle(command, id);

            return StatusCode(204);
        }
    }
}
