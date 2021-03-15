using LetItGrow.Microservice.Data.Nodes.Models;
using MediatR;

namespace LetItGrow.Microservice.Data.Nodes.Requests
{
    /// <summary>
    /// A request to get all nodes
    /// </summary>
    public record FindNodes : IRequest<NodeModel[]>
    {
    }
}