namespace Abd.Shared.Core.Exceptions;

public class UnprocessableException:AbdException
{
    public UnprocessableException(string description) : base("Unprocessable Entity", description, 422) { }
}