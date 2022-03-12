using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rebus.Activation;
using Rebus.Messages.Control;
using Rebus.Subscriptions;
using Rebus.Transport;

namespace Rebus.Handlers
{
    /// <summary>
    /// Decoration of <see cref="IHandlerActivator"/> that adds a few special handlers when an incoming message can be recognized
    /// as a special Rebus message
    /// </summary>
    class InternalHandlersContributor : IHandlerActivator
    {
        readonly IHandlerActivator _innerHandlerActivator;
        readonly Dictionary<Type, IEventHandler[]> _internalHandlers;

        public InternalHandlersContributor(IHandlerActivator innerHandlerActivator, ISubscriptionStorage subscriptionStorage)
        {
            _innerHandlerActivator = innerHandlerActivator;

            _internalHandlers = new Dictionary<Type, IEventHandler[]>
            {
                {typeof (SubscribeRequest), new IEventHandler[] {new SubscribeRequestHandler(subscriptionStorage)}},
                {typeof (UnsubscribeRequest), new IEventHandler[] {new UnsubscribeRequestHandler(subscriptionStorage)}}
            };
        }

        /// <summary>
        /// Gets Rebus' own internal handlers (if any) for the given message type
        /// </summary>
        public async Task<IEnumerable<IEventHandler<TMessage>>> GetHandlers<TMessage>(TMessage message, ITransactionContext transactionContext)
        {
            var ownHandlers = GetOwnHandlersFor<TMessage>();

            var handlers = await _innerHandlerActivator.GetHandlers(message, transactionContext);

            return handlers.Concat(ownHandlers);
        }

        IEnumerable<IEventHandler<TMessage>> GetOwnHandlersFor<TMessage>()
        {
            return _internalHandlers.TryGetValue(typeof(TMessage), out var ownHandlers)
                ? ownHandlers.OfType<IEventHandler<TMessage>>()
                : Enumerable.Empty<IEventHandler<TMessage>>();
        }

        class SubscribeRequestHandler : IEventHandler<SubscribeRequest>
        {
            readonly ISubscriptionStorage _subscriptionStorage;

            public SubscribeRequestHandler(ISubscriptionStorage subscriptionStorage)
            {
                _subscriptionStorage = subscriptionStorage;
            }

            public async Task Handle(SubscribeRequest message)
            {
                await _subscriptionStorage.RegisterSubscriber(message.Topic, message.SubscriberAddress);
            }
        }

        class UnsubscribeRequestHandler : IEventHandler<UnsubscribeRequest>
        {
            readonly ISubscriptionStorage _subscriptionStorage;

            public UnsubscribeRequestHandler(ISubscriptionStorage subscriptionStorage)
            {
                _subscriptionStorage = subscriptionStorage;
            }

            public async Task Handle(UnsubscribeRequest message)
            {
                await _subscriptionStorage.UnregisterSubscriber(message.Topic, message.SubscriberAddress);
            }
        }
    }
}