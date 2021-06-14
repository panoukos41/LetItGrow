using LetItGrow.Microservice.Common;
using LetItGrow.Microservice.Common.Models;
using LetItGrow.Microservice.Group.Models;
using LetItGrow.Microservice.Group.Requests;
using LetItGrow.Microservice.RestApi.Controllers.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.RestApi.Controllers
{
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    public class GroupController : ApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GroupModel[]))]
        public Task<IActionResult> Get([FromQuery] SearchGroups request, CancellationToken token) =>
            SendRequest(request, token);

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GroupModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
        public Task<IActionResult> Get(string id, CancellationToken token) =>
            SendRequest(new FindGroup { Id = id }, token);

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GroupModel))]
        public Task<IActionResult> Create([FromBody] CreateGroup request, CancellationToken token) =>
            SendCreateRequest(nameof(Get), r => new { id = r.Id }, request, token);

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ModelUpdate))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(Error))]
        public Task<IActionResult> Update([FromBody] UpdateGroup request, CancellationToken token) =>
            SendRequest(request, token);

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
        public Task<IActionResult> Delete([FromBody] DeleteGroup request, CancellationToken token) =>
            SendRequest(request, token);
    }
}