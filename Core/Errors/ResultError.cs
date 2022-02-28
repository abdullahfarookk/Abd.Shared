namespace Abd.Shared.Core.Errors;

public class ResultError
{
    public string? Message { get; set; }
    public Type ErrorType { get; set; } = default!;
}