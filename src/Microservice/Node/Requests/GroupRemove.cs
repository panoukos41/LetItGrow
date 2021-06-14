using LetItGrow.Microservice.Common.Requests;
using LetItGrow.Microservice.Node.Models;

namespace LetItGrow.Microservice.Node.Requests
{
    /// <summary>
    /// Remove a node from a group.
    /// </summary>
    public record GroupRemove : BaseUpdate
    {
        public GroupRemove()
        {
        }

        public GroupRemove(NodeModel node)
        {
            Id = node.Id;
            ConcurrencyStamp = node.ConcurrencyStamp;
        }
    }
}