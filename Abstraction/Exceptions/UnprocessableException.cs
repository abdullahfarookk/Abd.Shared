namespace Abd.Shared.Abstraction.Exceptions;

public class UnprocessableException:AbdException
{
    public UnprocessableException(string description) : base("Unprocessable Entity", description, 422) { }
}