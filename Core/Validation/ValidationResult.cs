namespace Abd.Shared.Core.Validation;

public interface IValidationResult{}
public class ValidationResult:IValidationResult
{
    public bool IsValid => Errors.Count < 1;
    public readonly List<ValidationError> Errors;
    public ValidationResult()
    {
        Errors = new List<ValidationError>();
    }
    public ValidationResult(List<ValidationError> errors)
    {
        Errors = errors;
    }

}