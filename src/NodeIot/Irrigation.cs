using NodaTime;
using ProtoBuf;

namespace Node.Data
{
    [ProtoContract]
    public class Irrigation
    {
        /// <summary>
        /// The id of the Node that this irrigation was created for.
        /// </summary>
        [ProtoMember(1)]
        public string NodeId { get; set; } = string.Empty;

        /// <summary>
        /// The exact momment in time this irrigation was created.
        /// </summary>
        [ProtoMember(2)]
        public Instant IssuedAt { get; set; }

        /// <summary>
        /// The type of this irrigation request.
        /// </summary>
        [ProtoMember(3)]
        public IrrigationType Type { get; set; }
    }
}