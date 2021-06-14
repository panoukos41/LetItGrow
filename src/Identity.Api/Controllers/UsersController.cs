using LetItGrow.Identity.Controllers.Internal;
using LetItGrow.Identity.User.Commands;
using LetItGrow.Identity.User.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Identity.Controllers
{
    [Route("api/v1/user")]
    public class UsersController : BaseController
    {
        [HttpGet("{id}")]
        public Task<IActionResult> Get(string id, CancellationToken cancellationToken) =>
            SendRequest(new GetUser { Id = id }, cancellationToken);

        [HttpGet]
        public Task<IActionResult> GetAll(CancellationToken cancellationToken) =>
            SendRequest(new GetUsers(), cancellationToken);

        [HttpPost]
        public Task<IActionResult> Create([FromBody] CreateUser request, CancellationToken cancellationToken) =>
            SendCreateRequest(request, nameof(Get), r => new { id = r.Id }, cancellationToken);

        //[HttpPut]
        //public Task<IActionResult> Update([FromForm] UpdateUser request, CancellationToken cancellationToken)

        [HttpDelete]
        public Task<IActionResult> Delete([FromBody] DeleteUser request, CancellationToken cancellationToken) =>
            SendRequest(request, cancellationToken);

        [HttpPut("role")]
        public Task<IActionResult> AddRole([FromBody] RoleAdd request, CancellationToken cancellationToken) =>
            SendRequest(request, cancellationToken);

        [HttpDelete("role")]
        public Task<IActionResult> RemoveRole([FromBody] RoleRemove request, CancellationToken cancellationToken) =>
            SendRequest(request, cancellationToken);
    }
}