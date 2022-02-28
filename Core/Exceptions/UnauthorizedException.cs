namespace Abd.Shared.Core.Exceptions;

public class UnauthorizedException : QuickException
{
    public UnauthorizedException(string description) : base("Unauthorized",description,401)
    {
    }
}