using MediatR;

namespace LetItGrow.Web.Data.NodeAuths.Requests
{
    public record InvalidateNodeAuth : IRequest
    {
        public InvalidateNodeAuth()
        {
            NodeId = string.Empty;
        }

        public string NodeId { get; init; }
    }
}