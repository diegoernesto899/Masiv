using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RouletteBettingAPI.Business.Interfaces;
using RouletteBettingAPI.Business.Model;
using System;
using System.Threading.Tasks;

namespace RouletteBettingAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RouletteController : ControllerBase
    {
        //private readonly IRouletteBusiness _accesBusinessLayer;
        private readonly ILogger<RouletteController> _logger;
        public RouletteController(/*IRouletteBusiness accesBusinessLayer,*/ ILogger<RouletteController> logger)
        {
            //_accesBusinessLayer = accesBusinessLayer ?? throw new ArgumentNullException(nameof(accesBusinessLayer));
            _logger = logger;
        }

        // GET: api/<RouletteController>
        [HttpGet]
        public async Task<ActionResult<RouletteModel>> CreateRoulette()
        {
            //var roulette = await _accesBusinessLayer.CreateRouletteRedisBusiness();
            //_logger.LogError("error netodo CreateRoulette ");
            return Ok();
        }

        // GET api/<RouletteController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RouletteController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RouletteController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RouletteController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
