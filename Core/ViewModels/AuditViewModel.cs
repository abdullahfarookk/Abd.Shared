namespace Abd.Shared.Core.ViewModels;

public class AuditViewModel
{
    public DateTime CreatedOn { get; set; }
    public Guid CreatedById { get; set; }
    public DateTime UpdatedOn { get; set; }
    public Guid UpdatedById { get; set; }
}