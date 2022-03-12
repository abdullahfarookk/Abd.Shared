using System.Collections.Generic;
using System.Threading.Tasks;
using Rebus.Handlers;
using Rebus.Transport;

namespace Rebus.Activation
{
    /// <summary>
    /// Responsible for creating handlers for a given message type
    /// </summary>
    public interface IHandlerActivator
    {
        /// <summary>
        /// Must return all relevant handler instances for the given message
        /// </summary>
#pragma warning disable CS3002 // Return type is not CLS-compliant
        Task<IEnumerable<IEventHandler<TMessage>>> GetHandlers<TMessage>(TMessage message, ITransactionContext transactionContext);
#pragma warning restore CS3002 // Return type is not CLS-compliant
    }
}