using LetItGrow.Microservice.Common.Requests;
using LetItGrow.Microservice.Node.Models;

namespace LetItGrow.Microservice.Node.Requests
{
    /// <summary>
    /// A request to get a single node using its id.
    /// </summary>
    public record FindNode : BaseFind<NodeModel>
    {
        public FindNode()
        {
        }

        public FindNode(string id)
        {
            Id = id;
        }
    }
}