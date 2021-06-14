using ProtoBuf;
using ProtoBuf.Meta;
using System;
using System.IO;
using System.Text.Json;

namespace LetItGrow.Microservice.Common
{
    /// <summary>
    /// Helper class to serialize/deserialize data to/from protobuf bytes.
    /// </summary>
    public static class ProtoSerializer
    {
        static ProtoSerializer()
        {
            RuntimeTypeModel.Default.Add(typeof(DateTimeOffset), false).SetSurrogate(typeof(DateTimeOffsetSurrogate));
        }

        /// <summary>
        /// Deserialize a protobuf proto array to T.
        /// the <see cref="ProtoContractAttribute"/> attribute.
        /// </summary>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <param name="proto">The protobuf to deserialize.</param>
        /// <returns>The class populated with values from the protobuf.</returns>
        public static T Deserialize<T>(byte[] proto) where T : class
        {
            return Serializer.Deserialize<T>(new ReadOnlyMemory<byte>(proto));
        }

        /// <summary>
        /// Serialize a class to a proto array.
        /// the <see cref="ProtoContractAttribute"/> attribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] Serialize<T>(T obj) where T : class
        {
            using var stream = new MemoryStream();
            Serializer.Serialize(stream, obj);
            return stream.ToArray();
        }

        /// <summary>
        /// Serialize a JsonDocument by first deserializing it to the provided class.
        /// the <see cref="ProtoContractAttribute"/> attribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static byte[] Serialize<T>(JsonDocument json) where T : class
        {
            return Serialize<T>(json.To<T>());
        }

        [ProtoContract]
        private sealed class DateTimeOffsetSurrogate
        {
            [ProtoMember(1)]
            public string? DateTimeString { get; set; }

            public static implicit operator DateTimeOffsetSurrogate(DateTimeOffset value)
            {
                return new DateTimeOffsetSurrogate { DateTimeString = value.ToUniversalTime().ToString("u") };
            }

            public static implicit operator DateTimeOffset(DateTimeOffsetSurrogate value)
            {
                return DateTimeOffset.Parse(value.DateTimeString);
            }
        }
    }
}