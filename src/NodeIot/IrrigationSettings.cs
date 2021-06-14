using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Node.Data
{
    [ProtoContract]
    public class IrrigationSettings : IEquatable<IrrigationSettings?>
    {
        /// <summary>
        /// A number in seconds indicating how often an Irrigation node polls.
        /// It polls new measurements and sends them to the server.<br/>
        /// <br/>
        /// Default is 1800 => 30 minutes.<br/>
        /// Min is 60       => 1 minute.<br/>
        /// Max is 21600    => 6 hours.
        /// </summary>
        [ProtoMember(1)]
        public int PollInterval { get; set; }

        /// <summary>
        /// Initialize a new <see cref="IrrigationSettings"/> record with default settings.
        /// </summary>
        public IrrigationSettings()
        {
            PollInterval = 1800;
        }

        /// <summary>
        /// Initialize a new <see cref="IrrigationSettings"/> record.
        /// </summary>
        public IrrigationSettings(int pollInterval)
        {
            PollInterval = pollInterval;
        }

        #region Equals

        public override bool Equals(object? obj)
        {
            return Equals(obj as IrrigationSettings);
        }

        public bool Equals(IrrigationSettings? other)
        {
            return other is not null &&
                   PollInterval == other.PollInterval;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PollInterval);
        }

        public static bool operator ==(IrrigationSettings? left, IrrigationSettings? right)
        {
            return EqualityComparer<IrrigationSettings>.Default.Equals(left, right);
        }

        public static bool operator !=(IrrigationSettings? left, IrrigationSettings? right)
        {
            return !(left == right);
        }

        #endregion
    }
}