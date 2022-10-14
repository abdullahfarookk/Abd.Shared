namespace Abd.Shared.Abstraction.Exceptions;

public class UnauthorizedException : AbdException
{
    public UnauthorizedException(string description) : base("Unauthorized",description,401)
    {
    }
}