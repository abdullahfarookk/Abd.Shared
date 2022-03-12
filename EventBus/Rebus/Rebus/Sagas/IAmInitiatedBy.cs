using Rebus.Handlers;

namespace Rebus.Sagas
{
    /// <summary>
    /// Derived marker interface, allowing for a handler to indicate that messages of type <typeparamref name="TMessage"/> 
    /// are allowed to instantiate new saga instances if the message cannot be correlated with an already existing instance
    /// </summary>
#pragma warning disable CS3027 // Type is not CLS-compliant because base interface is not CLS-compliant
    public interface IAmInitiatedBy<TMessage> : IEventHandler<TMessage> { }
#pragma warning restore CS3027 // Type is not CLS-compliant because base interface is not CLS-compliant
}