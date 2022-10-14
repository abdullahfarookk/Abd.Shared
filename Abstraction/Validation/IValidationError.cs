namespace Abd.Shared.Abstraction.Validation;

public interface IValidationError:IError
{
    /// <summary>
    /// The name of the property.
    /// </summary>
    public string Property { get; set; }
    
    /// <summary>
    /// The property value that caused the failure.
    /// </summary>
    public object? AttemptedValue { get; set; }

    /// <summary>
    /// Custom state associated with the failure.
    /// </summary>
    public object CustomState { get; set; }

    /// <summary>
    /// Gets or sets the formatted message placeholder values.
    /// </summary>
    public Dictionary<string, object> FormattedMessagePlaceholderValues { get; set; }

}