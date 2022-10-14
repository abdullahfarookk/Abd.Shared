namespace Abd.Shared.Abstraction.Validation;

public interface IValidationService
{
    IValidationResult Validate(object value);
}