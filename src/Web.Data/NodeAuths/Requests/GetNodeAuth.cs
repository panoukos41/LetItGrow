using LetItGrow.Web.Data.NodeAuths.Models;
using MediatR;

namespace LetItGrow.Web.Data.NodeAuths.Requests
{
    public record GetNodeAuth : IRequest<NodeAuthModel>
    {
        public GetNodeAuth()
        {
            NodeId = string.Empty;
        }

        public string NodeId { get; init; }
    }
}