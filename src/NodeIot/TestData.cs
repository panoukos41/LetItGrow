using NodaTime;
using ProtoBuf;

namespace Node.Data
{
    [ProtoContract(SkipConstructor = true)]
    public class TestData
    {
        public TestData(Instant time, string message)
        {
            Time = time;
            Message = message;
        }

        [ProtoMember(1)]
        public Instant Time { get; private set; }

        [ProtoMember(2)]
        public string Message { get; private set; }
    }
}