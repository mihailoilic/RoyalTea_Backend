using Microsoft.AspNetCore.Mvc;
using RoyalTea_Backend.Application;
using RoyalTea_Backend.Application.UseCases.Commands.Currencies;
using RoyalTea_Backend.Application.UseCases.DTO.Currency;
using RoyalTea_Backend.Application.UseCases.DTO.Searches;
using RoyalTea_Backend.Application.UseCases.Queries.Currencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RoyalTea_Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        public IUseCaseHandler handler { get; set; }

        public CurrencyController(IUseCaseHandler handler)
        {
            this.handler = handler;
        }


        // GET: api/<CurrencyController>
        [HttpGet]
        public IActionResult Get([FromQuery] PagedSearch request, [FromServices] IGetCurrencies query)
        {
            return Ok(this.handler.Handle(query, request));
        }



        // POST api/<CurrencyController>
        [HttpPost]
        public IActionResult Post([FromBody] CurrencyDto request, [FromServices] ICreateCurrency command)
        {
            this.handler.Handle(command, request);

            return StatusCode(201);
        }



        // DELETE api/<CurrencyController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteCurrency command)
        {
            this.handler.Handle(command, id);

            return StatusCode(204);
        }
    }
}
