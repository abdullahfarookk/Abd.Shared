namespace Abd.Shared.Core.Validation;

public interface IValidationService
{
    IValidationResult Validate(object value);
}