namespace Abd.Shared.Abstraction.Exceptions;

public class ConflictException : ApplicationException
{
    public ConflictException(string description) : base("Conflict",description, 409) { }
    public static ConflictException Create(string argument) => new($@"Provided argument: ""{argument}"" mismatch");
}