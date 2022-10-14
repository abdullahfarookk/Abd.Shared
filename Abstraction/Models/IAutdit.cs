namespace Abd.Shared.Abstraction.Models;

public interface IAudit:IModel
{
    DateTime CreatedOn { get; set; }
    DateTime ModifiedOn { get; set; }
    Guid CreatedBy { get; set; }
    Guid ModifiedBy { get; set; }
}