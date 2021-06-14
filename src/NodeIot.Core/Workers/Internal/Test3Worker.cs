using LetItGrow.NodeIot.Common;
using MQTTnet.Extensions.External.RxMQTT.Client;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.NodeIot.Workers.Internal
{
    /// <summary>
    /// Send mqtt custom topic messages.
    /// </summary>
    internal class Test3Worker : Worker
    {
        private readonly IRxMqttClient client;
        private readonly string _topic = "custom";
        private int _messageNum = 1;

        public Test3Worker(IRxMqttClient client) => this.client = client;

        protected override string Id { get; } = nameof(Test3Worker);

        protected override Task WorkAsync(CancellationToken stop)
        {
            //client.Connect(_topic)
            //    .ObserveOn(ThreadPoolScheduler.Instance)
            //    .Select(args => ProtoSerializer.Deserialize<TestData>(args.ApplicationMessage.Payload))
            //    .Subscribe(args => Log.Warning("Test({topic}) said {msg} at {time}", _topic, args.Message, args.Time));

            return new RepeatedTask(SendAsync, TimeSpan.FromSeconds(10), stop).Start();
        }

        protected async Task SendAsync(CancellationToken token)
        {
            //await client.PublishAsync(new ManagedMqttApplicationMessageBuilder()
            //    .WithApplicationMessage(msg => msg
            //        .WithAtLeastOnceQoS()
            //        .WithTopic(_topic)
            //        .WithPayload(ProtoSerializer.Serialize(new TestData(SystemClock.Instance.GetCurrentInstant(), _messageNum.ToString())))
            //        .WithRetainFlag())
            //    .Build());

            Log.Information("'{Id}' message '{num}' sent", Id, _messageNum++);
            await Task.CompletedTask;
        }
    }
}