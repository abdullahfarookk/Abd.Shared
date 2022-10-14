namespace Abd.Shared.Core.Entity;

public class Auditable:IAudit
{
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid ModifiedBy { get; set; }
}