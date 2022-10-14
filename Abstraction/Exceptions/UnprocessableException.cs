namespace Abd.Shared.Abstraction.Exceptions;

public class UnprocessableException:ApplicationException
{
    public UnprocessableException(string description) : base("Unprocessable Entity", description, 422) { }
}