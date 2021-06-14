using LetItGrow.Microservice.Common.Requests;
using LetItGrow.Microservice.Node.Models;

namespace LetItGrow.Microservice.Node.Requests
{
    /// <summary>
    /// A request to get all nodes
    /// </summary>
    public record SearchNodes : BaseSearch<NodeModel>
    {
        /// <summary>
        /// The group on which the nodes should belong to.
        /// </summary>
        public string? GroupId { get; set; }
    }
}