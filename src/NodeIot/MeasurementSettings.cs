using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Node.Data
{
    [ProtoContract]
    public class MeasurementSettings : IEquatable<MeasurementSettings?>
    {
        /// <summary>
        /// A number in seconds indicating how often a measurement node polls.
        /// It polls whether to stop irrigation. If no connection can be reached it will stop.<br/>
        /// <br/>
        /// Default is 600 => 10 minutes.<br/>
        /// Min is 60      => 1 minute.<br/>
        /// Max is 3600    => 1 hour.
        /// </summary>
        [ProtoMember(1)]
        public int PollInterval { get; set; }

        /// <summary>
        /// Initialize a new <see cref="MeasurementSettings"/> record with default settings.
        /// </summary>
        public MeasurementSettings()
        {
            PollInterval = 600;
        }

        /// <summary>
        /// Initialize a new <see cref="MeasurementSettings"/> record.
        /// </summary>
        public MeasurementSettings(int pollInterval)
        {
            PollInterval = pollInterval;
        }

        #region Equals

        public override bool Equals(object? obj)
        {
            return Equals(obj as MeasurementSettings);
        }

        public bool Equals(MeasurementSettings? other)
        {
            return other is not null &&
                   PollInterval == other.PollInterval;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PollInterval);
        }

        public static bool operator ==(MeasurementSettings? left, MeasurementSettings? right)
        {
            return EqualityComparer<MeasurementSettings>.Default.Equals(left!, right!);
        }

        public static bool operator !=(MeasurementSettings? left, MeasurementSettings? right)
        {
            return !(left == right);
        }

        #endregion
    }
}