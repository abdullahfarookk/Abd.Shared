using FluentValidation;
namespace Abd.Shared.Core.Validation;
public interface IValidation<in T>:IValidation,IValidator<T> { }
public class BaseValidator<T> : AbstractValidator<T>,IValidation<T>
{
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue =>
        async (model, propertyName) =>
        {
            var result =
                await ValidateAsync(
                    ValidationContext<T>.CreateWithOptions((T)model, x => x.IncludeProperties(propertyName)));
            return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
        };
}