using Abd.Shared.Core.Validation;
namespace Abd.Shared.Core.Services;

public abstract class BaseService:IService
{
    [Inject]
    private readonly IValidationService _validationService = null!;

    public virtual IValidationResult Validate(object model)
        => _validationService.Validate(model);

}