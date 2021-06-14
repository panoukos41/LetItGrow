using LetItGrow.Microservice.Mqtt.Services;
using LetItGrow.Microservice.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.Controllers
{
    [ApiController]
    public class ConnectedController : ControllerBase
    {
        private readonly INodeConnections nodeConnections;
        private readonly INodeTokenAuthenticator authenticator;

        public ConnectedController(INodeConnections nodeConnections, INodeTokenAuthenticator authenticator)
        {
            this.nodeConnections = nodeConnections;
            this.authenticator = authenticator;
        }

        [HttpGet("~/connected")]
        public async Task<IActionResult> GetConnectedNodes([FromForm] string user, [FromForm] string password) =>
            await authenticator.Authenticate(user, password)
                ? Ok(nodeConnections.GetConnectedIds())
                : Unauthorized();
    }
}