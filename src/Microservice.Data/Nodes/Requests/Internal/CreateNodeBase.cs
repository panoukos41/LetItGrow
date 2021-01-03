namespace LetItGrow.Microservice.Data.Nodes.Requests.Internal
{
    /// <summary>
    /// Base record for anything that has to do with a node request in general.<br/>
    /// This is only meant for internal use to create the public types.
    /// </summary>
    public abstract record CreateNodeBase
    {
        /// <summary>
        /// The name of the node.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The description of the node. (optional)
        /// </summary>
        public string? Description { get; set; }

        protected CreateNodeBase()
        {
        }
    }
}