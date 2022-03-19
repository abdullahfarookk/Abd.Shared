namespace Abd.Shared.Core.Exceptions;

public class UnauthorizedException : AbdException
{
    public UnauthorizedException(string description) : base("Unauthorized",description,401)
    {
    }
}