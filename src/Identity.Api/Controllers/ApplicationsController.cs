using LetItGrow.Identity.Application.Commands;
using LetItGrow.Identity.Application.Queries;
using LetItGrow.Identity.Controllers.Internal;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Identity.Controllers
{
    [Route("api/v1/app")]
    public class ApplicationsController : BaseController
    {
        [HttpGet("{id}")]
        public Task<IActionResult> Get(string id, CancellationToken cancellationToken) =>
            SendRequest(new GetApplication { Id = id }, cancellationToken);

        [HttpGet]
        public Task<IActionResult> GetAll(CancellationToken cancellationToken) =>
            SendRequest(new GetApplications(), cancellationToken);

        [HttpPost]
        public Task<IActionResult> Create([FromBody] CreateApplication request, CancellationToken cancellationToken) =>
            SendCreateRequest(request, nameof(Get), r => new { id = r.Id }, cancellationToken);

        [HttpPut]
        public Task<IActionResult> Update([FromBody] UpdateApplication request, CancellationToken cancellationToken) =>
            SendRequest(request, cancellationToken);

        [HttpDelete]
        public Task<IActionResult> Delete([FromBody] DeleteApplication request, CancellationToken cancellationToken) =>
            SendRequest(request, cancellationToken);
    }
}