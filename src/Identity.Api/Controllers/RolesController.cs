using LetItGrow.Identity.Controllers.Internal;
using LetItGrow.Identity.Role.Commands;
using LetItGrow.Identity.Role.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Identity.Controllers
{
    [Route("api/v1/role")]
    public class RolesController : BaseController
    {
        [HttpGet("{id}")]
        public Task<IActionResult> Get(string id, CancellationToken cancellationToken) =>
            SendRequest(new GetRole { Id = id }, cancellationToken);

        [HttpGet]
        public Task<IActionResult> GetAll(CancellationToken cancellationToken) =>
            SendRequest(new GetRoles(), cancellationToken);

        [HttpPost]
        public Task<IActionResult> Create([FromBody] CreateRole request, CancellationToken cancellationToken) =>
            SendCreateRequest(request, nameof(Get), r => new { id = r.Id }, cancellationToken);

        [HttpDelete]
        public Task<IActionResult> Delete([FromBody] DeleteRole request, CancellationToken cancellationToken) =>
            SendRequest(request, cancellationToken);
    }
}