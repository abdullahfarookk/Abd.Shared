namespace Abd.Shared.Core.Validation;


public class ValidationResult:IValidationResult
{
    private readonly IEnumerable<IValidationError> _errors;

    public ValidationResult()
    {
        _errors = Enumerable.Empty<IValidationError>();
    }
    public ValidationResult(IEnumerable<IValidationError> errors)
    {
        _errors = errors;
    }
    public bool IsSuccess  => _errors.Any();

    IEnumerable<IValidationError> IValidationResult.Errors => _errors;

    public IEnumerable<IError> Errors => _errors;
}