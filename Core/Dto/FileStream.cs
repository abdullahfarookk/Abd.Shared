namespace Abd.Shared.Core.Dto;
public class File : IDto
{
    public string Name { get; protected set; } = null!;
    public string? Label { get; set; }
    
    public int Length { get; protected set; }
    
    public string ContentType { get; protected set; } = null!;
}
public class FileBase64:File
{
    public FileBase64(string name, int length, string contentType, string base64)
    {
        Name = name;
        Length = length;
        ContentType = contentType;
        Base64 = base64;
    }

    public string Base64 { get; }
}
public sealed class FileStream : File
{
    public FileStream(string name, int length, string contentType, Stream stream)
    {
        Name = name;
        Length = length;
        ContentType = contentType;
        Stream = stream;
    }

    public Stream Stream { get; set; }
}

public class FileLimitDto
{
    public virtual int MaxSize { get; set; } = default!;

    public virtual string[] AllowedExtensions { get; set; } = default!;
}