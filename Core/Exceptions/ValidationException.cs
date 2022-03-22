using Abd.Shared.Core.Exceptions;

public class ValidationException : AbdException
{
    public ValidationException(string description) : base("Validation Error", description, 403) { }
}