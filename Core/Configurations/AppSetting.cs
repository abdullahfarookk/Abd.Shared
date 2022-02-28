namespace Abd.Shared.Core.Configurations;

public class AppSettings
{
    public IdentityAuthorization IdentityAuthorization { get; set; }
    public bool AgentJob { get; set; }
    public bool InMemoryEventBus { get; set; }
    public int PageSize { get; set; }
    public string ApplicationName { get; set; }
    public string HrPortalUrl { get; set; }
    public string SupportMail { get; set; }
    public string ApiUrl { get; set; }
    public string Authority { get; set; }
    public string Audience { get; set; }
    public string Secret { get; set; }
    public string EmailTemplatePath { get; set; }
    public string Environment { get; set; }
    public DomainSettings Domain { get; set; }
    public BlobSettings Blob { get; set; }
    public EmailAuth Email { get; set; }
    public string MicrosoftClientSecret { get; set; }
    public string MicrosoftClientId { get; set; }
    public string MicrosoftAuthority { get; set; }
}
public class BlobSettings
{
    public string ConnectionString { get; set; }
    public string QfBlobContainer { get; set; }

}
public class EmailAuth
{
    public string Email { get; set; }
    public string Password { get; set; }
}
public class DomainSettings
{
    public string ServerIp { get; set; }
    public string Record { get; set; }
    public string Credentials { get; set; }
}
public class IdentityAuthorization
{
    public string Authority { get; set; }
    public List<string> ValidIssuers { get; set; }
    public string Audience { get; set; }
    public string Secret { get; set; }
}