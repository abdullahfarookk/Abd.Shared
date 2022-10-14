namespace Abd.Shared.Abstraction.Exceptions;

public class ApplicationException : Exception
{
    public int Code { get;}
    public string Description { get; }

    public ApplicationException():base("Bad Request")
    {
        Code = 400;
        Description = "Cannot Process Request";
    }

    public ApplicationException(string message,string description,int code) : base(message)
    {
        Code = code;
        Description = description;
    }

    public ApplicationException(string message, string description,int code, Exception exception) : base(message,exception)
    {
        Code = code;
        Description = description;
    }
}