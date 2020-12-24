using LetItGrow.Web.Data.NodeAuths.Models;
using MediatR;

namespace LetItGrow.Web.Data.NodeAuths.Requests
{
    public record CreateNodeAuth : IRequest<NodeAuthModel>
    {
        public CreateNodeAuth()
        {
            NodeId = string.Empty;
        }

        public string NodeId { get; init; }
    }
}