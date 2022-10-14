namespace Abd.Shared.Core.Validation;


public class ValidationError : IValidationError
{

    /// <summary>
    /// Creates a new validation failure.
    /// </summary>
    public ValidationError(string propertyName, string errorMessage,string? description = null) : 
        this(propertyName, errorMessage, null,description)
    {
    }


    /// <summary>
    /// Creates a new ValidationFailure.
    /// </summary>
    public ValidationError(string propertyName, string errorMessage, object? attemptedValue,string? description = null)
    {
        Property = propertyName;
        Message = errorMessage;
        AttemptedValue = attemptedValue;
        Description = description;
    }


    /// <summary>
    /// The name of the property.
    /// </summary>
    public string Property { get; set; } = null!;

    /// <summary>
    /// The error message
    /// </summary>
    public string Message { get; set; } = null!;

    /// <summary>
    /// The property value that caused the failure.
    /// </summary>
    public object? AttemptedValue { get; set; } = null!;

    /// <summary>
    /// Custom state associated with the failure.
    /// </summary>
    public object CustomState { get; set; } = null!;

    /// <summary>
    /// Custom severity level associated with the failure.
    /// </summary>
    public Severity Severity { get; set; } = Severity.Error;
    

    /// <summary>
    /// Gets or sets the formatted message placeholder values.
    /// </summary>
    public Dictionary<string, object> FormattedMessagePlaceholderValues { get; set; } = null!;
    public string Code { get; set; } = "400";
    public string? Description { get; }
    public Exception? Exception { get; set; } = new ValidationException("Validation Error");

    /// <summary>
    /// Creates a textual representation of the failure.
    /// </summary>
    public override string ToString()
    {
        return Message;
    }
}
