using Abd.Shared.Core.Validation;

namespace Abd.Shared.Core.Services;

public interface IService
{
    IValidationResult Validate(object model);
}