namespace Abd.Shared.Abstraction.Validation;

public interface IValidation
{
    Func<object, string, Task<IEnumerable<string>>> ValidateValue { get; }
}