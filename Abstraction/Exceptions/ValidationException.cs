namespace Abd.Shared.Abstraction.Exceptions;

public class ValidationException : AbdException
{
    public ValidationException(string description) : base("Validation Error", description, 403) { }
}