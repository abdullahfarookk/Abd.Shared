namespace Abd.Shared.Core.Validation;

public class ValidationError
{

    public ValidationError()
    {

    }

    /// <summary>
    /// Creates a new validation failure.
    /// </summary>
    public ValidationError(string propertyName, string errorMessage) : this(propertyName, errorMessage, null)
    {
    }

    /// <summary>
    /// Creates a new ValidationFailure.
    /// </summary>
    public ValidationError(string propertyName, string errorMessage, object attemptedValue)
    {
        PropertyName = propertyName;
        ErrorMessage = errorMessage;
        AttemptedValue = attemptedValue;
    }

    /// <summary>
    /// The name of the property.
    /// </summary>
    public string PropertyName { get; set; } = null!;

    /// <summary>
    /// The error message
    /// </summary>
    public string ErrorMessage { get; set; } = null!;

    /// <summary>
    /// The property value that caused the failure.
    /// </summary>
    public object AttemptedValue { get; set; } = null!;

    /// <summary>
    /// Custom state associated with the failure.
    /// </summary>
    public object CustomState { get; set; } = null!;

    /// <summary>
    /// Custom severity level associated with the failure.
    /// </summary>
    public Severity Severity { get; set; } = Severity.Error;

    /// <summary>
    /// Gets or sets the error code.
    /// </summary>
    public string ErrorCode { get; set; } = null!;

    /// <summary>
    /// Gets or sets the formatted message placeholder values.
    /// </summary>
    public Dictionary<string, object> FormattedMessagePlaceholderValues { get; set; } = null!;

    /// <summary>
    /// Creates a textual representation of the failure.
    /// </summary>
    public override string ToString()
    {
        return ErrorMessage;
    }
}
/// <summary>Specifies the severity of a rule.</summary>
public enum Severity
{
    /// <summary>Error</summary>
    Error,
    /// <summary>Warning</summary>
    Warning,
    /// <summary>Info</summary>
    Info,
}