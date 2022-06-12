using Microsoft.AspNetCore.Mvc;
using RoyalTea_Backend.Application;
using RoyalTea_Backend.Application.UseCases.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RoyalTea_Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        public IUseCaseHandler handler { get; set; }
        public ISeed seedCommand { get; set; }

        public SeedController(
            IUseCaseHandler handler,
            ISeed seedCommand
            )
        {
            this.handler = handler;
            this.seedCommand = seedCommand;
        }

        // POST api/<SeedController>
        [HttpPost]
        public void Post()
        {
            this.handler.Handle(seedCommand);
        }

    }
}
