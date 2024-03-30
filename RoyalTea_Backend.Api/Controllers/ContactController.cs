using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RoyalTea_Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        // GET: api/<ContactController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }


        // POST api/<ContactController>
        [HttpPost]
        public IActionResult Post()
        {
            return StatusCode(201);
        }

    }
}
