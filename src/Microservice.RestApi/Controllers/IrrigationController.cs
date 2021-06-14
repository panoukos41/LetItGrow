using LetItGrow.Microservice.Common;
using LetItGrow.Microservice.Irrigation.Models;
using LetItGrow.Microservice.Irrigation.Requests;
using LetItGrow.Microservice.RestApi.Controllers.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.RestApi.Controllers
{
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    public class IrrigationController : ApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IrrigationModel[]))]
        public Task<IActionResult> Search([FromBody] SearchIrrigations request, CancellationToken token) =>
            SendRequest(request, token);

        [HttpGet("many")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IrrigationModel[]))]
        public Task<IActionResult> SearchMany([FromBody] SearchManyIrrigations request, CancellationToken token) =>
            SendRequest(request, token);
    }
}