namespace Abd.Shared.Core;

public class AppSettings
{
    public bool InMemoryEventBus { get; set; }
    public int PageSize { get; set; }
    public ThisApplication This { get; set; } = null!;
    public string SupportMail { get; set; } = null!;
    public string ApiUrl { get; set; } = null!;
    public string EmailTemplatePath { get; set; } = null!;
    public string Environment { get; set; } = null!;
    public DomainSettings Domain { get; set; } = null!;
    public BlobSettings Blob { get; set; } = null!;
    public EmailAuth Email { get; set; } = null!;
    public Microsoft Microsoft { get; set; } = null!;   
}

public abstract class ThisApplication
{
    public string Name { get; set; } = null!;
    public string Environment { get; set; } = null!;
    public string Authority { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public string Secret { get; set; } = null!;
    
}

public abstract class Microsoft
{
    public string ClientSecret { get; set; } = null!;
    public string ClientId { get; set; } = null!;
    public string Authority { get; set; } = null!;
} 
public class AuthSettings
{
    public string Authority { get; set; } = null!;
    public List<string> ValidIssuers { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public string Secret { get; set; } = null!;
} 
public abstract class BlobSettings
{
    public string ConnectionString { get; set; } = null!;
    public string QfBlobContainer { get; set; } = null!;

} 
public abstract class EmailAuth
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
public abstract class DomainSettings
{
    public string ServerIp { get; set; } = null!;
    public string Record { get; set; } = null!;
    public string Credentials { get; set; } = null!;
} 