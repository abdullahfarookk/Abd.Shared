namespace Abd.Shared.Core.Validation;

public class DataAnnotationValidation:IValidationService
{
    /// <summary>
    /// To Validate ViewModels
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public IValidationResult Validate(object model)
    {
        var context = new ValidationContext(model, serviceProvider: null, items: null);
        var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
        var isValid = Validator.TryValidateObject(model, context, results);
        if (!isValid)
            return new ValidationResult(results.Select(s => new ValidationError
            {
                ErrorMessage = s?.ErrorMessage!,
                PropertyName = s?.MemberNames?.FirstOrDefault()!
            }).ToList());
        return new ValidationResult();
    }
}