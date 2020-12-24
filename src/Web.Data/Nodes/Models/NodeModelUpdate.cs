namespace LetItGrow.Web.Data.Nodes.Models
{
    /// <summary>
    /// Record returned when an update request is sent.
    /// </summary>
    public record NodeModelUpdate
    {
        /// <summary>
        /// The id of the node that was updated.
        /// </summary>
        public string Id { get; init; } = string.Empty;

        /// <summary>
        /// The new ConcurrencyStamp the node should use.
        /// </summary>
        public uint ConcurrencyStamp { get; init; }
    }
}