namespace Abd.Shared.Core.ViewModels;

public class FileViewModel: IViewModel

{
    public virtual string Name { get; set; } = default!;

    public virtual MemoryStream Stream { get; set; } = default!;

    public virtual string Label { get; set; } = default!;
    public virtual string Extension { get; set; } = default!;
}

public class FileLimit
{
    public virtual int Length { get; set; }
    public virtual int Count { get; set; }
}