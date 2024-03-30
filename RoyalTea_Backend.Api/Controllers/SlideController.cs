using Microsoft.AspNetCore.Mvc;
using RoyalTea_Backend.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RoyalTea_Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlideController : ControllerBase
    {
        // GET: api/<SlideController>
        [HttpGet]
        public IActionResult Get([FromServices] AppDbContext context)
        {
            return Ok(context.Slides.Where(x => x.IsActive).ToList());
        }

    }
}
