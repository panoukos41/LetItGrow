using LetItGrow.Microservice.Common;
using LetItGrow.Microservice.Measurement.Models;
using LetItGrow.Microservice.Measurement.Requests;
using LetItGrow.Microservice.RestApi.Controllers.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.RestApi.Controllers
{
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    public class MeasurementController : ApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MeasurementModel[]))]
        public Task<IActionResult> Search([FromBody] SearchMeasurements request, CancellationToken token) =>
            SendRequest(request, token);
        
        [HttpGet("many")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MeasurementModel[]))]
        public Task<IActionResult> SearchMany([FromBody] SearchManyMeasurements request, CancellationToken token) =>
            SendRequest(request, token);
    }
}