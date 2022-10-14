namespace Abd.Shared.Abstraction.Exceptions;

public class UnauthorizedException : ApplicationException
{
    public UnauthorizedException(string description) : base("Unauthorized",description,401)
    {
    }
}