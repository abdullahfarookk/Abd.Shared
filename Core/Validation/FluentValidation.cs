namespace Abd.Shared.Core.Validation;

public class FluentValidation:IValidationService
{
    [Inject]
    private readonly IServiceProvider _services = null!;

    /// <summary>
    /// To Validate ViewModels
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public virtual IValidationResult Validate(object model)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));
        var validator = GetValidator(model);
        if (validator == null)
            throw new NotFoundException($"Validator not found of Type {model.GetType()}Validator");
        var validationContext = typeof(ValidationContext<>);
        var objType = model.GetType();
        var genericType = validationContext.MakeGenericType(objType);


        var context = (IValidationContext)Activator.CreateInstance(genericType, model)!;

        var result = validator.Validate(context);
        return new ValidationResult(result.Errors.Select(x =>
            new ValidationError
            {
                AttemptedValue = x.AttemptedValue,
                CustomState = x.CustomState,
                ErrorCode = x.ErrorCode,
                ErrorMessage = x.ErrorMessage,
                FormattedMessagePlaceholderValues = x.FormattedMessagePlaceholderValues,
                PropertyName = x.PropertyName,
                Severity = x.Severity,
            }).ToList());
    }
    private IValidator? GetValidator(object obj)
    {
        var abstractValidatorType = typeof(AbstractValidator<>);
        var objType = obj.GetType();
        var genericType = abstractValidatorType.MakeGenericType(objType);
        return (IValidator)FindValidatorType(genericType)!;
    }

    private object? FindValidatorType(Type genericType)
    {
        if (genericType == null) throw new ArgumentNullException(nameof(genericType));
        return _services.GetService(genericType);
    }


}