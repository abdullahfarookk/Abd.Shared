namespace Abd.Shared.Abstraction;

public interface ICommonFilter:IFilter
{
    public string? SearchTerm { get; set; }
    public byte? Status { get; set; }
  
}