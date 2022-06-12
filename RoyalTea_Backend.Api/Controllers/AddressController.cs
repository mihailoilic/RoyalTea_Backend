﻿using Microsoft.AspNetCore.Mvc;
using RoyalTea_Backend.Application;
using RoyalTea_Backend.Application.UseCases.Commands.Addresses;
using RoyalTea_Backend.Application.UseCases.DTO;
using RoyalTea_Backend.Application.UseCases.DTO.Searches;
using RoyalTea_Backend.Application.UseCases.Queries.Addresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RoyalTea_Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        public IUseCaseHandler handler { get; set; }
        public ICreateAddress createAddressCommand { get; set; }
        public IUpdateAddress updateAddressCommand { get; set; }
        public IDeleteAddress deleteAddressCommand { get; set; }
        public IGetAddresses getAddressesQuery { get; set; }

        public AddressController(IUseCaseHandler handler, ICreateAddress createAddressCommand, IUpdateAddress updateAddressCommand, IDeleteAddress deleteAddressCommand, IGetAddresses getAddressesQuery)
        {
            this.handler = handler;
            this.createAddressCommand = createAddressCommand;
            this.updateAddressCommand = updateAddressCommand;
            this.deleteAddressCommand = deleteAddressCommand;
            this.getAddressesQuery = getAddressesQuery;
        }


        // GET: api/<AddressController>
        [HttpGet]
        public IActionResult Get([FromQuery] PagedSearch request)
        {
            return Ok(this.handler.Handle(this.getAddressesQuery, request));
        }


        // POST api/<AddressController>
        [HttpPost]
        public IActionResult Post([FromBody] AddAddressDto request)
        {
            this.handler.Handle(this.createAddressCommand, request);

            return StatusCode(201);
        }

        // PUT api/<AddressController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateAddressDto request)
        {
            request.Id = id;
            this.handler.Handle(this.updateAddressCommand, request);

            return StatusCode(204);
        }

        // DELETE api/<AddressController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            this.handler.Handle(deleteAddressCommand, id);

            return StatusCode(204);
        }
    }
}
