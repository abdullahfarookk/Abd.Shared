namespace Abd.Shared.Abstraction;

public interface IValidation
{
    Func<object, string, Task<IEnumerable<string>>> ValidateValue { get; }
}