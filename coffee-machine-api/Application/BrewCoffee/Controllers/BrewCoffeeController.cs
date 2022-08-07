using System;
using coffee_machine_api.Application.BrewCoffee.Queries.BrewCoffee;
using coffee_machine_api.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace coffee_machine_api.Application.BrewCoffee.Controllers
{
    [ApiController]
    [Route("api")]
    public class BrewCoffeeController : ApiControllerBase
    {
        [HttpGet]
        [Route("brew-coffee")]
        public async Task<IActionResult> BrewCoffee()
        {
            return Ok(await Mediator.Send(new BrewCoffeeQuery()));
        }
    }
}

