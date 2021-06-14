using LetItGrow.Microservice.Common.Requests;
using LetItGrow.Microservice.Node.Models;

namespace LetItGrow.Microservice.Node.Requests
{
    /// <summary>
    /// A request to delete a node using its id and concurrency stamp to validate the deletation.
    /// </summary>
    public record DeleteNode : BaseDelete
    {
        public DeleteNode()
        {
        }

        public DeleteNode(NodeModel node)
        {
            Id = node.Id;
            ConcurrencyStamp = node.ConcurrencyStamp;
        }
    }
}