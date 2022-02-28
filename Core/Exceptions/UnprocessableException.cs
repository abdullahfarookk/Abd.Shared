namespace Abd.Shared.Core.Exceptions;

public class UnprocessableException:QuickException
{
    public UnprocessableException(string description) : base("Unprocessable Entity", description, 422) { }
}