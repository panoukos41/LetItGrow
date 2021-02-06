using LetItGrow.Services;
using StackExchange.Redis;
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace LetItGrow.CoreHost.Services
{
    public class RedisService : IDistributedCache, IPubSubService
    {
        private readonly ConnectionMultiplexer _connection;
        private readonly IDatabase _redis;
        private readonly ISubscriber _subscriber;

        public RedisService(ConfigurationOptions options)
        {
            _connection = ConnectionMultiplexer.Connect(options);
            _redis = _connection.GetDatabase();
            _subscriber = _connection.GetSubscriber();
        }

        /// <inheritdoc/>
        public Task KeyAdd(string key, string value)
        {
            return _redis.StringSetAsync(key, value);
        }

        /// <inheritdoc/>
        public Task KeyDelete(string key)
        {
            return _redis.KeyDeleteAsync(key);
        }

        /// <inheritdoc/>
        public async Task<string?> KeyGet<T>(string key)
        {
            var value = await _redis.StringGetAsync(key);

            if (value.HasValue)
            {
                return value;
            }
            return null;
        }

        /// <inheritdoc/>
        public Task ListAdd(string list, string value)
        {
            return _redis.SetAddAsync(list, value);
        }

        /// <inheritdoc/>
        public Task ListDelete(string list, string value)
        {
            return _redis.SetRemoveAsync(list, value);
        }

        /// <inheritdoc/>
        public async Task<string[]> ListMembers(string list)
        {
            return (await _redis.SetMembersAsync(list))
                .Cast<string>()
                .ToArray();
        }

        /// <inheritdoc/>
        public Task Pub(string channel, string message)
        {
            return _redis.PublishAsync(channel, message);
        }

        /// <inheritdoc/>
        public IObservable<string> Sub(string channel) => Observable.Create<string>(async (obs) =>
        {
            // https://stackoverflow.com/questions/40789943/observable-stream-from-stackexchange-redis-pub-sub-subscription
            // as the SubscribeAsync callback can be invoked concurrently
            // a thread-safe wrapper for OnNext is needed.

            var syncObs = Observer.Synchronize(obs, true);
            await _subscriber
                .SubscribeAsync(channel, (_, value) => syncObs.OnNext(value))
                .ConfigureAwait(false);

            return Disposable.Create(() => _subscriber.Unsubscribe(channel));
        });
    }
}