using FuturesClean.API.Code.Comunication.Enum;
using FuturesClean.API.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace FuturesClean.API.Areas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseEndPointController : ControllerBase
    {
        private readonly IFuturesDifferenceService _futuresDifferenceService;

        public BaseEndPointController(IFuturesDifferenceService futuresDifferenceService)
        {
            _futuresDifferenceService = futuresDifferenceService;
        }

        [HttpPost("FuturesDifference/")]
        public async Task<IActionResult> DeleteFuturesDifference([FromQuery] string interval, [FromQuery] DateTime utcTime, [FromQuery] string symbols)
        {
            var resourse = await _futuresDifferenceService.CalculateFuturesDifferenceAsync(interval, utcTime, symbols);
            if (resourse.InnerStatusCode == InnerStatusCode.FuturesDifferenceCreate)
            {
                return Ok(resourse.Data);
            }

            if (resourse.InnerStatusCode == InnerStatusCode.EntityNotFound)
            {
                return Ok(resourse.Message);
            }

            return StatusCode(500);
        }

        [HttpGet("FuturesDifference/")]
        public async Task<IActionResult> GetFuturesDifferences()
        {
            var resourse = await _futuresDifferenceService.GetFuturesDifferencesAsync(x => x.Id != null);
            if (resourse.InnerStatusCode == InnerStatusCode.FuturesDifferenceRead)
            {
                return Ok(resourse.Data);
            }

            return StatusCode(500);
        }

        [HttpDelete("FuturesDifference/{id}")]
        public async Task<IActionResult> DeleteFuturesDifference([FromRoute] Guid id)
        {
            var resourse = await _futuresDifferenceService.DeleteFuturesDifferenceAsync(id);
            if (resourse.InnerStatusCode == InnerStatusCode.FuturesDifferenceDelete)
            {
                return Ok(resourse.Data);
            }
            if (resourse.InnerStatusCode == InnerStatusCode.EntityNotFound)
            {
                return Ok(resourse.Message);
            }

            return StatusCode(500);
        }
    }
}
