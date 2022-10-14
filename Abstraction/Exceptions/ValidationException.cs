namespace Abd.Shared.Abstraction.Exceptions;

public class ValidationException : ApplicationException
{
    public ValidationException(string description) : base("Validation Error", description, 403) { }
}