namespace Abd.Shared.Core.Dto;

public class FileDto : IDto
{
    public virtual string Name { get; set; } = default!;
    public virtual MemoryStream Stream { get; set; } = default!;
    public virtual string Label { get; set; } = default!;
    public virtual string Extension { get; set; } = default!;
    public string Size { get; set;  }= default!;
}

public class FileLimitDto
{
    public virtual int MaxSize { get; set; } = default!;

    public virtual string[] AllowedExtensions { get; set; } = default!;
}