using LetItGrow.Microservice.Common.Requests;
using LetItGrow.Microservice.Node.Models;

namespace LetItGrow.Microservice.Node.Requests
{
    public record RefreshToken : BaseUpdate<RefreshModel>
    {
        public RefreshToken()
        {
        }

        public RefreshToken(NodeModel node)
        {
            Id = node.Id;
            ConcurrencyStamp = node.ConcurrencyStamp;
        }
    }
}