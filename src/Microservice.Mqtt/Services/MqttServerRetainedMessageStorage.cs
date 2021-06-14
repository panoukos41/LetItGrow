using MQTTnet;
using MQTTnet.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.Mqtt.Services
{
    public class MqttServerRetainedMessageStorage : IMqttServerStorage
    {
        private static readonly string Filename = Path.Combine(Environment.CurrentDirectory, "RetainedMessages.json");

        public async Task SaveRetainedMessagesAsync(IList<MqttApplicationMessage> messages)
        {
            await File.WriteAllTextAsync(Filename, JsonConvert.SerializeObject(messages));
        }

        public async Task<IList<MqttApplicationMessage>> LoadRetainedMessagesAsync()
        {
            return File.Exists(Filename)
                ? JsonConvert.DeserializeObject<List<MqttApplicationMessage>>(await File.ReadAllTextAsync(Filename))!
                : new List<MqttApplicationMessage>();
        }
    }
}