using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoyalTea_Backend.Api.Core;
using RoyalTea_Backend.Api.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RoyalTea_Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private JwtManager jwtManager;

        public TokenController(JwtManager jwtManager)
        {
            this.jwtManager = jwtManager;
        }


        // POST api/<TokenController>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] TokenRequest request)
        {
            return Ok(new { Token = this.jwtManager.CreateToken(request.Username, request.Password) });
        }

    }
}
