namespace Abd.Shared.Core.Exceptions;

public class ConflictException : QuickException
{
    public ConflictException(string description) : base("Conflict",description, 409) { }
    public static ConflictException Create(string argument) => new ConflictException($@"Provided argument: ""{argument}"" mismatch");
}