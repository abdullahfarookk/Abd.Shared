using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rebus.Extensions;
using Rebus.Messages;
using Rebus.Serialization;

namespace EventBus.Rebus.Configurations;

class CommonMessageDeserializer : ISerializer
{
    /// <summary>
    /// If the type name found in the '<see cref="Type"/>' header can be found in this dictionary, the incoming
    /// message will be deserialized into the specified type
    /// </summary>
    public static ConcurrentDictionary<string, Type> KnownTypes = new ConcurrentDictionary<string, Type>();
    readonly ISerializer _serializer;

    public CommonMessageDeserializer(ISerializer serializer) => _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));

    public Task<TransportMessage> Serialize(Message message) => _serializer.Serialize(message);

    public Task<Message> Deserialize(TransportMessage transportMessage)
    {
        var headers = transportMessage.Headers.Clone();
        var json = Encoding.UTF8.GetString(transportMessage.Body);
        var typeName = headers.GetValue(Headers.Type);

        // Check for integration events
        if (KnownTypes.TryGetValue(typeName, out var type)) 
            return CreateMessage(headers, json, type);

        type = Type.GetType(typeName);
        return type is null
            ? CreateMessage(headers, json)
            : CreateMessage(headers, json, type);
    }

    private Task<Message> CreateMessage(Dictionary<string, string> headers, string json, Type? type = null)
        => Task.FromResult(type is null // if we don't know the type, just deserialize the message into a JObject
            ? new Message(headers, JsonConvert.DeserializeObject<JObject>(json))
            : new Message(headers, JsonConvert.DeserializeObject(json, type!)));
}