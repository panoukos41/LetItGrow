using ProtoBuf;
using ProtoBuf.Meta;
using System;
using System.IO;

namespace Node.Data
{
    /// <summary>
    /// Helper class to serialize/deserialize data to/from protobuf bytes.
    /// </summary>
    public static class ProtoSerializer
    {
        static ProtoSerializer()
        {
            RuntimeTypeModel.Default.AddNodaTime();
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
    }
}