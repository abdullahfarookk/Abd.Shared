namespace Abd.Shared.Core.ViewModels;

public class FileViewModel: IBaseViewModel

{
    [Required] public string Name { get; set; } = default!;

    [Required] public MemoryStream Stream { get; set; } = default!;

    public string Label { get; set; } = default!;
}

public class FileLimit
{
    public int Length { get; set; }
    public int Count { get; set; }
}