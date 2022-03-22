public interface IError
{
    /// <summary>
    /// The name of the property.
    /// </summary>
    public string? Property { get; set; }

    public Exception? Exception { get; set; }
    /// <summary>
    /// The error message
    /// </summary>
    public string Message { get; set; }


    /// <summary>
    /// Gets or sets the error code.
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// Custom severity level associated with the failure.
    /// </summary>
    public Severity Severity { get; set; }

}