using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RouletteBettingAPI.Business.Implementation;
using RouletteBettingAPI.Business.Interfaces;
using RouletteBettingAPI.Business.Model;
using System;
using System.Threading.Tasks;

namespace RouletteBettingAPI.Controllers
{
    [Route("roulleteAPI/v1/[controller]")]
    [ApiController]
    public class RouletteController : ControllerBase
    {
        #region PrivateMethos
        private readonly IRouletteBusiness _accessBusinessLayer;
        private readonly ILogger<RouletteController> _logger;

        private void CatchErrorLog(Exception ex)
        {
            _logger.LogError(ex.Message);
        }
        private bool ValidIfUserIsAuthorizedToDoAction()
        {
            //Logic to valid if the user is authorized to do action.
            return true;
        }
        #endregion
        #region public methods
        public RouletteController(IRouletteBusiness accesBusinessLayer, ILogger<RouletteController> logger)
        {
            _accessBusinessLayer = accesBusinessLayer ?? throw new ArgumentNullException(nameof(accesBusinessLayer));
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<RouletteModel>> CreateRoulette()
        {
            try
            {
                if (ValidIfUserIsAuthorizedToDoAction())
                {
                    var getIdRouletteCreated = await _accessBusinessLayer.CreateRouletteBusiness();
                    return Ok(getIdRouletteCreated);
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                CatchErrorLog(ex);
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }

        [HttpPut("{idRoulette}")]
        public IActionResult OpenRouletteByID(int idRoulette)
        {
            try
            {
                if (ValidIfUserIsAuthorizedToDoAction())
                {
                    _accessBusinessLayer.RouletteOpeningByIDBusiness(idRoulette);
                    return Ok();
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                CatchErrorLog(ex);
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
        #endregion

    }
}
