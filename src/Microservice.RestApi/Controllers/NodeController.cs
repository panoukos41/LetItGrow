using LetItGrow.Microservice.Common;
using LetItGrow.Microservice.Common.Models;
using LetItGrow.Microservice.Node.Models;
using LetItGrow.Microservice.Node.Requests;
using LetItGrow.Microservice.RestApi.Controllers.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.RestApi.Controllers
{
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
    public class NodeController : ApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NodeModel[]))]
        public Task<IActionResult> Search([FromQuery] SearchNodes request, CancellationToken token) =>
            SendRequest(request, token);

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NodeModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
        public Task<IActionResult> Get(string id, CancellationToken token) =>
            SendRequest(new FindNode { Id = id }, token);

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(NodeModel))]
        public Task<IActionResult> Create([FromBody] CreateNode request, CancellationToken token) =>
            SendCreateRequest(nameof(Get), r => new { id = r.Id }, request, token);

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ModelUpdate))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(Error))]
        public Task<IActionResult> Update([FromBody] UpdateNode request, CancellationToken token) =>
            SendRequest(request, token);

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(Error))]
        public Task<IActionResult> Delete([FromBody] DeleteNode request, CancellationToken token) =>
            SendNoContentRequest(request, token);

        [HttpPatch("add")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ModelUpdate))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(Error))]
        public Task<IActionResult> GroupAdd([FromBody] GroupAdd request, CancellationToken token) =>
            SendRequest(request, token);

        [HttpPatch("remove")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ModelUpdate))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(Error))]
        public Task<IActionResult> GroupRemove(GroupRemove request, CancellationToken token) =>
            SendRequest(request, token);

        [HttpPatch("refresh")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RefreshModel))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(Error))]
        public Task<IActionResult> RefreshToken(RefreshToken request, CancellationToken token) =>
            SendRequest(request, token);
    }
}