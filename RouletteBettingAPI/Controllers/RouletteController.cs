using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RouletteBettingAPI.Business.Interfaces;
using RouletteBettingAPI.Business.Model;
using RouletteBettingAPI.CrossCutting.Model;
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
        private readonly IValidParametersRequestEndpoints _validParameters;
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
        public RouletteController(IRouletteBusiness accessBusinessLayer, ILogger<RouletteController> logger, IValidParametersRequestEndpoints validParameters)
        {
            _validParameters = validParameters ?? throw new ArgumentNullException(nameof(validParameters));
            _accessBusinessLayer = accessBusinessLayer ?? throw new ArgumentNullException(nameof(accessBusinessLayer));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("CreateRoulette")]
        public async Task<ActionResult<int>> CreateRoulette()
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

        [HttpPut("OpenRoulette")]
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

        [HttpPost("PlaceBet")]
        public IActionResult MakeBet(RequestBetRouletteModel betObject)
        {
            try
            {
                var foundErrorInValidations = _validParameters.checkParametersOfBetRequest(betObject);
                if (!foundErrorInValidations.Equals(string.Empty))
                    return BadRequest(foundErrorInValidations);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}"); ;
            }

        }
        #endregion

    }
}
