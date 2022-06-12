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
    public class UserController : ControllerBase
    {

        public IUseCaseHandler handler { get; set; }
        public IGetUsersQuery getUsersQuery { get; set; }

        public UserController(IUseCaseHandler handler, IGetUsersQuery getUsersQuery)
        {
            this.handler = handler;
            this.getUsersQuery = getUsersQuery;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get([FromQuery] PagedSearch request)
        {
            var result = handler.Handle(this.getUsersQuery, request);
            return Ok(result);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
