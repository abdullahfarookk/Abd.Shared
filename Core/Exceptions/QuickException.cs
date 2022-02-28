namespace Abd.Shared.Core.Exceptions;

public class QuickException : Exception
{
    public int Code { get;}
    public string Description { get; private set; }

    public QuickException():base("Bad Request")
    {
        Code = 400;
        Description = "Cannot Process Request";
    }

    public QuickException(string message,string description,int code) : base(message)
    {
        Code = code;
        Description = description;
    }

    public QuickException(string message, string description,int code, Exception exception) : base(message,exception)
    {
        Code = code;
        Description = description;
    }
}