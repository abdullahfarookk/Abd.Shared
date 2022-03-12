namespace Rebus.Handlers.Reordering
{
    /// <summary>
    /// Configurer returned from <see cref="ReorderingConfiguration.First{THandler}"/> that allows for specifying additional handler types
    /// whose order to fix
    /// </summary>
    public class AdditionalReorderingConfiguration
    {
        readonly ReorderingConfiguration _reorderingConfiguration;

        internal AdditionalReorderingConfiguration(ReorderingConfiguration reorderingConfiguration)
        {
            _reorderingConfiguration = reorderingConfiguration;
        }

        /// <summary>
        /// Specifies the handler that will be put next in the pipeline if it is present - call <see cref="Then{THandler}"/>
        /// again to specify the next handler
        /// </summary>
#pragma warning disable CS3024 // Constraint type is not CLS-compliant
        public AdditionalReorderingConfiguration Then<THandler>() where THandler : IEventHandler
#pragma warning restore CS3024 // Constraint type is not CLS-compliant
        {
            _reorderingConfiguration.Add<THandler>();
            return this;
        }
    }
}