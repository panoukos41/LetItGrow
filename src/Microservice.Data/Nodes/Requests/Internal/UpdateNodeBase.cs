using LetItGrow.Microservice.Data.Nodes.Models;

namespace LetItGrow.Microservice.Data.Nodes.Requests.Internal
{
    /// <summary>
    /// Base record for anything that has to do with a node request in general.<br/>
    /// This is only meant for internal use to create the public types.
    /// </summary>
    public abstract record UpdateNodeBase
    {
        /// <summary>
        /// The id of the node.
        /// </summary>
        public string Id { get; init; } = string.Empty;

        /// <summary>
        /// The nodes concurrency stamp to validate if the node is up to date.
        /// </summary>
        public uint ConcurrencyStamp { get; init; }

        /// <summary>
        /// A new name for the node.
        /// </summary>
        public string? Name { get; init; }

        /// <summary>
        /// A new description for the node.
        /// </summary>
        public string? Description { get; init; }

        protected UpdateNodeBase()
        {
        }

        protected UpdateNodeBase(NodeModel node)
        {
            Id = node.Id;
            ConcurrencyStamp = node.ConcurrencyStamp;
        }
    }
}