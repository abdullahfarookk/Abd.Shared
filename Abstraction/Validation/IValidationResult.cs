namespace Abd.Shared.Abstraction.Validation;

public interface IValidationResult : IResult
{
    public new IEnumerable<IValidationError> Errors { get; }
}