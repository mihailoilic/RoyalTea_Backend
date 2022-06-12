using Microsoft.AspNetCore.Mvc;
using RoyalTea_Backend.Application;
using RoyalTea_Backend.Application.UseCases.Commands;
using RoyalTea_Backend.Application.UseCases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RoyalTea_Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        public IUseCaseHandler  handler { get; set; }
        public IRegisterUser registerCommand { get; set; }

        public RegisterController(IUseCaseHandler handler, IRegisterUser registerCommand)
        {
            this.handler = handler;
            this.registerCommand = registerCommand;
        }

        // POST api/<RegisterController>
        [HttpPost]
        public IActionResult Post([FromBody] RegisterDto request)
        {
            this.handler.Handle(this.registerCommand, request);

            return StatusCode(201);
        }

    }
}
