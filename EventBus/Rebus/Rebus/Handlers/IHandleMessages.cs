using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Rebus.Handlers
{
    ///// <summary>
    ///// Base message handler interface. Don't implement this one directly, it would give you nothing
    ///// </summary>
    //public interface IEventHandler { }
    
    ///// <summary>
    ///// Message handler interface. Implement this in order to get to handle messages of a specific type
    ///// </summary>
    //[UsedImplicitly(ImplicitUseTargetFlags.Itself | ImplicitUseTargetFlags.WithInheritors)]
    //public interface IEventHandler<in TMessage> : IEventHandler
    //{
    //    /// <summary>
    //    /// This method will be invoked with a message of type <typeparamref name="TMessage"/>
    //    /// </summary>
    //    Task Handle(TMessage message);
    //}
}